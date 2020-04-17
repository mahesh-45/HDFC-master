using HDFC.Core.Dtos.Common;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HDFC.Core.Repositories.Masters
{
    public interface IBudgetHeadRepository : IActiveRepository<BudgetHead>
    {
        Task<bool> AnyAsync(BudgetHead budgetHead);
        Task<BudgetHeadListDto> GetList(SearchDto searchDto);
        Task<List<BudgetHead>> GetParents();
        Task<List<BudgetHead>> GetChildren(long parentId);
    }
}
