using HDFC.Core.Entities.Masters;
using System.Threading.Tasks;
using HDFC.Core.Dtos.Masters;

namespace HDFC.Core.Repositories.Masters
{
    public interface ICostCodeRepository : IActiveRepository<CostCode>
    {
        Task<bool> AnyAsync(CostCode costCode);

        Task<CostCodeListDto> GetList( string search);

    }
}
