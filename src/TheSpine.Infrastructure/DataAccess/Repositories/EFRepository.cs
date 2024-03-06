using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSpine.Core.Common;
using TheSpine.Infrastructure.DataAccess.Contract;

namespace TheSpine.Infrastructure.DataAccess.Repositories
{
    public class EFRepository<T> : IAsyncRepository<T> where T : IdentifiableEntity
    {
        private readonly ApplicationDbContext dbContext;

        public EFRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> AddAsync(T entity)
        {
            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteAsync(T entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
           return await dbContext.Set<T>().SingleAsync(x => x.Id == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
