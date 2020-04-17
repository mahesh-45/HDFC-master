using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HDFC.Core.Repositories.Masters
{
    public interface ICurrencyRepository : IActiveRepository<Currency>
    {
        Task<bool> AnyAsync(Currency currency);

        Task<CurrencyListDto> GetList(string sort, string order, int page, int pageSize, string search);
    }
}
