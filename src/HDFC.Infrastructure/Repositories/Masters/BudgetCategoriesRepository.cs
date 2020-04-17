using HDFC.Core.Dtos.Common;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Enums;
using HDFC.Core.Repositories;
using HDFC.Core.Repositories.Masters;
using HDFC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace HDFC.Infrastructure.Repositories.Masters
{
    public class BudgetCategoriesRepository : ActiveRepository<BudgetCategory>, IBudgetCategoriesRepository
    {
        public BudgetCategoriesRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> AnyAsync(BudgetCategory budgetCategory)
        {
            if (budgetCategory.Id > 0)
            {
                return await _dbContext.BudgetHeads.AnyAsync(c => c.Id != budgetCategory.Id && c.Name == budgetCategory.Name && c.Code == budgetCategory.Code);
            }
            else
            {
                return await _dbContext.BudgetHeads.AnyAsync(c => c.Name == budgetCategory.Name && c.Code == budgetCategory.Code);
            }
        }

        public async Task<BudgetCategoryListDto> GetList(SearchDto searchDto)
        {
            BudgetCategoryListDto res = new BudgetCategoryListDto();

            var SkipPage = searchDto.Page * searchDto.PageSize;

            List<BudgetCategoryDto> budgetCategory = await (from a in _dbContext.BudgetCategories
                                                            where (searchDto.Search != null ? (a.Name.Contains(searchDto.Search) || a.Code.Contains(searchDto.Search)) : true)
                                                            select new BudgetCategoryDto()
                                                            {
                                                                Name = a.Name,
                                                                Code = a.Code,
                                                                Status = a.Status,
                                                                Id = a.Id,
                                                                Uid = a.Uid,
                                                                CreatedDate = a.CreatedDate
                                                            }).ToListAsync();
            res.Total_count = budgetCategory.Count();

            switch (searchDto.Sort + "_" + searchDto.Order)
            {
                case "name_desc":
                    res.Items = budgetCategory.Skip(SkipPage).Take(searchDto.PageSize).OrderByDescending(s => s.Name).ToList();
                    break;
                case "name_asc":
                    res.Items = budgetCategory.Skip(SkipPage).Take(searchDto.PageSize).OrderBy(s => s.Name).ToList();
                    break;
                case "code_desc":
                    res.Items = budgetCategory.Skip(SkipPage).Take(searchDto.PageSize).OrderByDescending(s => s.Code).ToList();
                    break;
                case "code_asc":
                    res.Items = budgetCategory.Skip(SkipPage).Take(searchDto.PageSize).OrderBy(s => s.Code).ToList();
                    break;
                case "createdDate_desc":
                    res.Items = budgetCategory.Skip(SkipPage).Take(searchDto.PageSize).OrderByDescending(s => s.CreatedDate).ToList();
                    break;
                case "createdDate_asc":
                    res.Items = budgetCategory.Skip(SkipPage).Take(searchDto.PageSize).OrderBy(s => s.CreatedDate).ToList();
                    break;
            }

            return res;
        }
    }
}
