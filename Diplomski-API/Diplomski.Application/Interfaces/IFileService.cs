using Diplomski.Application.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Application.Interfaces
{
    public interface IFileService
    {
        Task<FileDto> UploadFile(CreateFileDto file, UserDto user);
        Task<List<FileExtradataDto>> GetUserFilesAsync(UserDto user, PaginationParameters paginationParameters);
    }
}
