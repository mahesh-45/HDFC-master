using HDFC.Core.Dtos.Procurements;
using HDFC.Core.Entities.Budgeting;
using HDFC.Core.Repositories.Procurements;
using HDFC.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HDFC.Core.Dtos.Common;
using HDFC.Core.Dtos.Procurements.Budgeting;

namespace HDFC.Infrastructure.Repositories.Procurements
{
    public class BudgetRepository : Repository<Budget>, IBudgetRepository
    {
        public BudgetRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> AnyAsync(Budget budget)
        {
            return await _dbContext.Budgets.AnyAsync(c => c.Name == budget.Name && c.ReferenceId == budget.ReferenceId && c.IsCurrent == true);
        }

        public async Task<Budget> GetBudgets(string uid)
        {
            return await _dbContext.Budgets.Include(x => x.SubBudgets).Include(x => x.BudgetSpendLimits).Include(x => x.BudgetCostCodes).Where(x => x.Uid == uid).SingleOrDefaultAsync();
        }

        public async Task<BudgetListDto> GetList(SearchDto searchDto)
        {
            BudgetListDto res = new BudgetListDto();
            List<BudgetDto> budgets = await (from a in _dbContext.Budgets.Where(c => c.IsCurrent == true)
                                             where (searchDto.Search != null ? (a.Name.Contains(searchDto.Search) || a.ReferenceId.Contains(searchDto.Search)) : true)
                                             select new BudgetDto()
                                             {
                                                 Name = a.Name,
                                                 Description = a.Description,
                                                 ReferenceId = a.ReferenceId,
                                                 BudgetStatus = a.BudgetStatus,
                                                 BudgetType = a.BudgetType,
                                                 TotalAmount = a.TotalAmount,
                                                 CreatedDate = a.CreatedDate,
                                                 StartDate = a.StartDate,
                                                 EndDate = a.EndDate,
                                                 CreatedBy = a.CreatedBy,
                                                 Id = a.Id,
                                                 Uid = a.Uid,
                                                 CurrencyId = a.CurrencyId,
                                             }).OrderByDescending(c => c.Id).ToListAsync();
            res.Total_count = budgets.Count();
            res.Items = budgets;
            return res;
        }
    }

}
