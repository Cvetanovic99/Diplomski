using Diplomski.Application.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Application.Interfaces.ThirdPartyContracts
{
    public interface IStorageService
    {
        Task<FileDto> UploadAsync(IFormFile formFile, string nameWithoutExtension = null);
        Task DeleteAsync(string filePath);
        bool FileExist(string filePath);
    }
}
