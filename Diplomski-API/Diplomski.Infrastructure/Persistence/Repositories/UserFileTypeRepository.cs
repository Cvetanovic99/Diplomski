using Diplomski.Application.Interfaces.ThirdPartyContracts;
using Diplomski.Core.Entities;
using Diplomski.Core.Entities.ManyToManyRelations;
using Diplomski.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Infrastructure.Persistence.Repositories
{
    public class UserFileTypeRepository : AsyncRepository<UserFileType>, IUserFileTypeRepository
    {
        public UserFileTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<UserFileType> DoesUserContainFileTypeAsync(int userId, string fileType)
        {
            return  await _dbContext.UserFileTypes.Where(userFileType => userFileType.User.Id == userId && userFileType.FileType.Type == fileType).Include(userFileType => userFileType.FileType).FirstOrDefaultAsync<UserFileType>();
        }

        public async Task<ICollection<UserFileType>> GetAllUserFileTypesAsync(int userId)
        {
           return await _dbContext.UserFileTypes.Where(userFileType => userFileType.User.Id == userId).Include(userFileType => userFileType.FileType).ToListAsync();
     
        }
    }
}
