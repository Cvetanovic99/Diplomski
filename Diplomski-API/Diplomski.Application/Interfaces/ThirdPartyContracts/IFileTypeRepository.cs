using Diplomski.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Application.Interfaces.ThirdPartyContracts
{
    public interface IFileTypeRepository : IAsyncRepository<FileType>
    {
        Task<FileType> GetByTypeAsync(string type);
    }
}
