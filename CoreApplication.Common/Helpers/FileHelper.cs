using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreApplication.Common
{
    public static class FileHelper
    {

        public static string UploadFile(IHostingEnvironment hostingEnvironment, IFormFile file)
        {
            var uploads = Path.Combine(hostingEnvironment.ContentRootPath, "Uploads");

            var filePath = string.Empty;

            if (file.Length > 0)
            {
                filePath = Path.Combine(uploads, file.FileName);

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return filePath;
        }

        public static string UploadFile(IFormFile file)
        {
            var url = new StringBuilder();

            if (file != null)
                using (var ms = new MemoryStream())
                {
                    url.Append("data:image/png;base64, ");
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    url.Append(Convert.ToBase64String(fileBytes));
                }

            return url.ToString();
        }


    }
}
