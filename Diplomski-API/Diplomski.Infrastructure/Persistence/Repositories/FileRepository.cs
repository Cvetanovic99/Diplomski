using AutoMapper;
using Diplomski.Application.Dtos;
using Diplomski.Application.Interfaces.ThirdPartyContracts;
using Diplomski.Core.Entities;
using Diplomski.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Infrastructure.Persistence.Repositories
{
    public class FileRepository : AsyncRepository<File>, IFileRepository
    {
        private readonly IMapper _mapper;

        public FileRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            this._mapper = mapper;
        }

        public async Task<FileExtradataDto> GetFileExtradataAsync(PaginationParameters paginationParameters, int userId, string fileType)
        { 
            var files = await _dbContext.Files.Where(file => file.Owner.Id == userId && file.Type == fileType).Skip(paginationParameters.From).Take(paginationParameters.To - paginationParameters.From).ToListAsync();

            return new FileExtradataDto { FileType = fileType, Files = _mapper.Map<ICollection<FileDto>>(files), Count = 0 };
           
        }
        public async Task<List<string>> GetAllUserFileTypesAsync(int userId)
        {
            return await _dbContext.Files.Where(file => file.Owner.Id == userId).Select(file => file.Type).Distinct().ToListAsync();
        }

        public async Task<int> GetCountOfUserFileType(string fileType, int userId)
        {
            return await _dbContext.Files.Where(file => file.Owner.Id == userId && file.Type == fileType).CountAsync();
        }
    }
}
