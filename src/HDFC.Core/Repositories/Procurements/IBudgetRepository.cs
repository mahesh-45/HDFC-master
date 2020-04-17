using HDFC.Core.Dtos.Common;
using HDFC.Core.Dtos.Procurements.Budgeting;
using HDFC.Core.Entities.Budgeting;
using System.Threading.Tasks;

namespace HDFC.Core.Repositories.Procurements
{
    public interface IBudgetRepository : IRepository<Budget>
    {
        Task<BudgetListDto> GetList(SearchDto searchDto);
        Task<bool> AnyAsync(Budget budget);
        Task<Budget> GetBudgets(string uid);
    }
}
