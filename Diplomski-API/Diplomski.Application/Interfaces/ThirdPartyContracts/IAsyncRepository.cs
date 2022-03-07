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
        Task AddAsync(T entity);
        Task UpdateAsync(T entiy);
        Task<IList<T>> GetAsync();
        ValueTask DisposeAsync(bool disposing);
        Task DeleteAsync(T entity);
    }
}
