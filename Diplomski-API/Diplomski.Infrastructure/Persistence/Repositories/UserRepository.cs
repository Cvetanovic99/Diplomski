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
    public class UserRepository : AsyncRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<User> GetUserByIdentityId(string identityId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.IdentityId == identityId);
        }

        public async Task<FileType> GetUserFileTypes(int userId, string fileType)
        {
            //var userFromDatabase = await _dbContext.Users.Where(user => user.Id == userId).Include(user => user.FileTypes).FirstOrDefaultAsync();
            //foreach (var userFileType in userFromDatabase.FileTypes)
            //{
            //    if (userFileType.Type == fileType)
            //        return userFileType;
            //}

            //return null;
            throw new NotImplementedException("NotImplemented");

        }

        public async Task<ICollection<FileType>> GetAllUserFileTypesAsync(int userId)
        {
            //var user = await _dbContext.Users.Where(user => user.Id == userId).Include(user => user.FileTypes).FirstOrDefaultAsync();
            //return user.FileTypes; 
            throw new NotImplementedException("NotImplemented");
        }


    }
}
