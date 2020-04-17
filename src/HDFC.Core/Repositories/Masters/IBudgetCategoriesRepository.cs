using HDFC.Core.Dtos.Common;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using System.Threading.Tasks;

namespace HDFC.Core.Repositories
{
    public interface IBudgetCategoriesRepository : IActiveRepository<BudgetCategory>
    {
        Task<BudgetCategoryListDto> GetList(SearchDto searchDto);
        Task<bool> AnyAsync(BudgetCategory budgetCategory);

    }
}
