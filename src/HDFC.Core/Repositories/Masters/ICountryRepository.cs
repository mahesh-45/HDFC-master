using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HDFC.Core.Repositories.Masters
{
    public interface ICountryRepository : IActiveRepository<Country>
    {
        Task<bool> AnyAsync(Country country);
        Task<CountryListDto> GetList(string sort, string order, int page, int pageSize, string search);
    }
}
