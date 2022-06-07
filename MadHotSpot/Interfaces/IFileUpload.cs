using MadHotSpot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Interfaces
{
    public interface IFileUpload
    {
        Task<ResultJson> UploadFileImage(string upDirectory, string filePath, string name);
    }
}
