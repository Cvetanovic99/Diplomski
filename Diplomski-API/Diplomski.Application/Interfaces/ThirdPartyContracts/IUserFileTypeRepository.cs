using Diplomski.Core.Entities;
using Diplomski.Core.Entities.ManyToManyRelations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Application.Interfaces.ThirdPartyContracts
{
    public interface IUserFileTypeRepository : IAsyncRepository<UserFileType>
    {
        Task<UserFileType> DoesUserContainFileTypeAsync(int userId, string fileType);
        Task<ICollection<UserFileType>> GetAllUserFileTypesAsync(int userId);
    }
}
