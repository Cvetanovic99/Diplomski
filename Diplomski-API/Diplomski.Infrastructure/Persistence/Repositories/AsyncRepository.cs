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
    public class AsyncRepository<T> : IAsyncRepository<T>
        where T : BaseEntity
    {
        protected AppDbContext _dbContext;

        public AsyncRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
        }

        public async ValueTask DisposeAsync(bool disposing)
        {
            if (disposing && _dbContext != null)
            {
                await _dbContext.DisposeAsync()
                    .ConfigureAwait(false);
                _dbContext = null;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        //public async Task<T> GetSingleBySpecAsync(string IdentityId)
        //{
        //    return await _dbContext.Set<T>().Where(u => u.IdentityId == IdentityId).FirstOrDefaultAsync();
        //}

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<T>> GetAsync()
        {
           return await _dbContext.Set<T>().ToListAsync();
        }
    }
}
