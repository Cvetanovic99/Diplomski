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
    public class FileTypeRepository : AsyncRepository<FileType>, IFileTypeRepository
    {
        public FileTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<FileType> GetByTypeAsync(string type)
        {
            return await _dbContext.FileTypes.Where(fileType => fileType.Type == type).FirstOrDefaultAsync<FileType>();
        }
    }
}
