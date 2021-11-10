using Diplomski.Application.Exceptions;
using Diplomski.Application.Interfaces.ThirdPartyContracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Infrastructure.Storage.Services
{
    public class StorageService : IStorageService
    {
        private readonly IWebHostEnvironment _environment;

        public StorageService(IWebHostEnvironment environment)
        {
            this._environment = environment;    
        }
        public Task DeleteAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadAsync(IFormFile formFile, string nameWithoutExtension = null)
        {
            try
            {
                var extension = Path.GetExtension(formFile.FileName);
                var fileName = $"{nameWithoutExtension ?? Guid.NewGuid().ToString()}{extension}";
                var path = Path.Combine(_environment.ContentRootPath, "uploads").ToLower();

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var fullFileLocation = Path.Combine(path, fileName).ToLower();

                using (var fileStream = new FileStream(fullFileLocation, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                return $"uploads/{fileName}";

            }
            catch(Exception) 
            {
                throw new ApiException("Unable to upload file", 500);
            }
        }
    }
}
