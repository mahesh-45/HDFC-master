using HDFC.Core.Repositories;
using HDFC.Core.SharedKernel;
using HDFC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HDFC.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        protected readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> SingleAsync(long id)
        {
            return await _dbContext.Set<T>().SingleAsync(e => e.Id == id);
        }

        public async Task<T> FindAsync(long id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> ToListAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<T> SingleAsync(string uid)
        {
            return await _dbContext.Set<T>().SingleAsync(e => e.Uid == uid);
        }
    }
}
