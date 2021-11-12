using Diplomski.Application.Dtos;
using Diplomski.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Application.Interfaces.ThirdPartyContracts
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetUserByIdentityId(string identityId);
        Task<FileType> GetUserFileTypes(int userId, string fileType);
    }
}
