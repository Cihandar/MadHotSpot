using MadHotSpot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using MadHotSpot.Models;

namespace MadHotSpot.Extentions
{
    public class FileUpload : IFileUpload
    {

        private readonly IWebHostEnvironment _env;

        public FileUpload(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<ResultJson> UploadFileImage(string upDirectory,string filePath, string name)
        {
        
            try
            {
                var uploadDirecotroy = upDirectory;
                var uploadPath = Path.Combine(_env.WebRootPath, uploadDirecotroy);

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                name = Guid.NewGuid().ToString() + "-" + name;
                var bytes = Convert.FromBase64String(filePath.Split(',')[1]);

                var fileName = Path.Combine(uploadPath, name);

                File.WriteAllBytes(fileName, bytes); 

                return new ResultJson { Success = true, Message = name };
            }
            catch (Exception ex)
            {
                return new ResultJson { Success = false, Message = ex.Message };
            }
        }


    }
}
