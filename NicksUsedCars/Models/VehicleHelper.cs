using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NicksUsedCars.Models
{
    public static class VehicleHelper
    {
        public static async void AddPhoto(Vehicle v, IHostingEnvironment env)
        {
            var photo = v.Photo;
            // check file extension is valid
            string extension = Path.GetExtension(photo.FileName);
            if (extension == ".png" || extension == ".jpg" || extension == ".jpeg")
            {
                // TODO: set up image resizing to cut on data
                // possibly use ImageSharp 

                // generate unique name for photo to avoid name collision
                string newFileName = Guid.NewGuid().ToString();

                // store photo in fule system and reference in DB
                if (photo.Length > 0)
                {
                    // path of image
                    string filePath = Path.Combine(env.WebRootPath, "images", newFileName + extension);

                    // save location to database (in URL format)
                    v.PhotoUrl = "images/" + newFileName + extension;

                    // write file to file system
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(fs);
                    }
                }
            }
        }
    }
}
