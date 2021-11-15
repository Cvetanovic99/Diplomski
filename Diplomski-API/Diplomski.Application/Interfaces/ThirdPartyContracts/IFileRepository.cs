using Diplomski.Application.Dtos;
using Diplomski.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Application.Interfaces.ThirdPartyContracts
{
    public interface IFileRepository : IAsyncRepository<File>
    {
        Task<FileExtradataDto> GetFileExtradataAsync(PaginationParameters paginationParameters, int userId, string fileType);
    }
}
