using Diplomski.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Application.Interfaces.ThirdPartyContracts
{
    public interface IAsyncRepository<T> : IAsyncDisposable
        where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        //Task<T> GetSingleBySpecAsync(string IdentityId);
        Task AddAsync(T entity);
        Task UpdateAsync(T entiy);
        ValueTask DisposeAsync(bool disposing);
    }
}
