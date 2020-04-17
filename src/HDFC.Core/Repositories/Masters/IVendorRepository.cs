
using System.Collections.Generic;
using System.Threading.Tasks;
using HDFC.Core.Dtos.Masters.Vendor;
using HDFC.Core.Entities.Masters.Vendor;


namespace HDFC.Core.Repositories.Masters
{
    public interface IVendorRepository : IActiveRepository<Vendor>
    {
        Task<bool> AnyAsync(Vendor vendor);
        Task<VendorListDto> GetList(string sort, string order, int page, int pageSize, string search);
    }
}
