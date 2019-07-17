using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Processing;

namespace NicksUsedCars.Models
{
    public static class VehicleHelper
    {
        // set max size for image to be 500pixels each side
        private const int BigImageLength = 500;
        // max size for small image to be 200pixels each side
        private const int SmallImageLength = 200;

        public static async void AddPhoto(Vehicle v, IHostingEnvironment env)
        {
            // save photo to images and connect it to smallPhotoUrl in database
            string photoUrl = await SavePhoto(v, env, "small");
            // load image  to be resized
            using (Image<Rgba32> image = Image.Load(photoUrl))
                ResizePhoto(v, photoUrl, SmallImageLength, image);

            //save photo to images and connect it to photoUrl in database
            photoUrl = await SavePhoto(v, env, "big");
            // load image to be resized
            using (Image<Rgba32> image = Image.Load(photoUrl))
                ResizePhoto(v, photoUrl, BigImageLength, image);

        }

        private static async Task<string> SavePhoto(Vehicle v, IHostingEnvironment env, string typeOfPhoto)
        {
            var photo = v.Photo;
            // check file extension is valid
            string extension = Path.GetExtension(photo.FileName);
            if (extension == ".png" || extension == ".jpg" || extension == ".jpeg")
            {
                // generate unique name for photo to avoid name collision
                string newFileName = Guid.NewGuid().ToString();

                // store photo in fule system and reference in DB
                if (photo.Length > 0)
                {
                    // path of image
                    string filePath = Path.Combine(env.WebRootPath, "images", newFileName + extension);

                    // save location to database (in URL format)
                    if (typeOfPhoto.Equals("big"))
                    {
                        v.PhotoUrl = "images/" + newFileName + extension;
                    }
                    else
                    {
                        v.SmallPhotoUrl = "images/" + newFileName + extension;
                    }

                    // write file to file system
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(fs);
                    }

                    // return filepath so it may be uploaded again to be resized
                    return filePath;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private static void ResizePhoto(Vehicle v, string photoUrl, int maxPhotoDimension, Image<Rgba32> image)
        {
            // set resize options
            ResizeOptions photoUrlResize = new ResizeOptions
            {
                // option to set resized image to fit inside size dimensions w/o losing aspect ratio
                Mode = ResizeMode.Max,
                // set max size of image when resized
                Size = new SixLabors.Primitives.Size(maxPhotoDimension, maxPhotoDimension)
            };
            // resize image to options set
            image.Mutate(ctx => ctx.Resize(photoUrlResize));
            // save image to same location loaded from
            image.Save(photoUrl);
        }
    }
}
