using HDFC.Core.Interfaces;
using HDFC.Core.Repositories;
using HDFC.Core.SharedKernel;
using HDFC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Infrastructure.Repositories
{
    public class ActiveRepository<T> : Repository<T>, IActiveRepository<T> where T : AggregateRoot, IStatus
    {
        public ActiveRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<T>> ActiveToListAsync()
        {
            return await _dbContext.Set<T>().Where(s => s.Status == Core.Enums.StatusEnum.Active).ToListAsync();
        }

    }
}
