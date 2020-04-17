using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Repositories.Masters;
using HDFC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDFC.Infrastructure.Repositories.Masters
{
    class CurrencyRepository : ActiveRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AnyAsync(Currency currency)
        {
            if (currency.Id > 0)
            {
                return await _dbContext.Currencies.AnyAsync(c => c.Id != currency.Id && c.Name == currency.Name && c.Code == currency.Code);
            }
            else
            {
                return await _dbContext.Currencies.AnyAsync(c => c.Name == currency.Name && c.Code == currency.Code);
            }
        }

        public async Task<CurrencyListDto> GetList(string sort, string order, int page, int pageSize, string search)
        {

            CurrencyListDto res = new CurrencyListDto();

            List<CurrencyDto> currencies = await (from a in _dbContext.Currencies
                                                  where (search != null ? (a.Name.Contains(search)
                                                 || a.Code.Contains(search)) : true)
                                                 select new CurrencyDto()
                                                 {
                                                     Code = a.Code,
                                                     Name = a.Name,
                                                     Description = a.Description,
                                                     Status = a.Status,
                                                     CreatedDate = a.CreatedDate,
                                                     Id = a.Id,
                                                     Uid = a.Uid

                                                 }).ToListAsync();
            res.Total_count = currencies.Count();
            res.Items = currencies;

            return res;
        }
    }
}
