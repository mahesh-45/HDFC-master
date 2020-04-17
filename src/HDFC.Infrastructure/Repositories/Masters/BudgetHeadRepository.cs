using HDFC.Core.Dtos.Common;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Enums;
using HDFC.Core.Repositories.Masters;
using HDFC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Infrastructure.Repositories.Masters
{
    public class BudgetHeadRepository : ActiveRepository<BudgetHead>, IBudgetHeadRepository
    {
        public BudgetHeadRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AnyAsync(BudgetHead budgetHead)
        {
            if (budgetHead.Id > 0)
            {
                return await _dbContext.BudgetHeads.AnyAsync(c => c.Id != budgetHead.Id && c.Name == budgetHead.Name && c.Code == budgetHead.Code);
            }
            else
            {
                return await _dbContext.BudgetHeads.AnyAsync(c => c.Name == budgetHead.Name || c.Code == budgetHead.Code);
            }
        }

        public async Task<List<BudgetHead>> GetChildren(long parentId) 
        {
            return await _dbContext.BudgetHeads.Where(x => x.ParentId == parentId && x.Status == StatusEnum.Active).ToListAsync();
        }

        public async Task<BudgetHeadListDto> GetList(SearchDto searchDto)
        {
            BudgetHeadListDto res = new BudgetHeadListDto();

            var SkipPage = searchDto.Page * searchDto.PageSize;

            List<BudgetHeadDto> budgetHeads = await (from a in _dbContext.BudgetHeads
                                                     where (searchDto.Search != null ? (a.Name.Contains(searchDto.Search) || a.Code.Contains(searchDto.Search)) : true)
                                                     select new BudgetHeadDto()
                                                     {
                                                         Name = a.Name,
                                                         Code = a.Code,
                                                         ParentId = a.ParentId == null?0:a.ParentId,
                                                         CostCodeId = a.CostCodeId,
                                                         DepartmentId = a.DepartmentId,
                                                         Status = a.Status,
                                                         Id = a.Id,
                                                         Uid = a.Uid,
                                                         CreatedDate = a.CreatedDate
                                                     }).ToListAsync();
            res.Total_count = budgetHeads.Count();

            switch (searchDto.Sort + "_" + searchDto.Order)
            {
                case "name_desc":
                    res.Items = budgetHeads.Skip(SkipPage).Take(searchDto.PageSize).OrderByDescending(s => s.Name).ToList();
                    break;
                case "name_asc":
                    res.Items = budgetHeads.Skip(SkipPage).Take(searchDto.PageSize).OrderBy(s => s.Name).ToList();
                    break;
                case "code_desc":
                    res.Items = budgetHeads.Skip(SkipPage).Take(searchDto.PageSize).OrderByDescending(s => s.Code).ToList();
                    break;
                case "code_asc":
                    res.Items = budgetHeads.Skip(SkipPage).Take(searchDto.PageSize).OrderBy(s => s.Code).ToList();
                    break;
                case "createdDate_desc":
                    res.Items = budgetHeads.Skip(SkipPage).Take(searchDto.PageSize).OrderByDescending(s => s.CreatedDate).ToList();
                    break;
                case "createdDate_asc":
                    res.Items = budgetHeads.Skip(SkipPage).Take(searchDto.PageSize).OrderBy(s => s.CreatedDate).ToList();
                    break;
            }

            return res;
        }

        public async Task<List<BudgetHead>> GetParents()
        {
            return await _dbContext.BudgetHeads.Where(x => x.ParentId == null  && x.Status == StatusEnum.Active).ToListAsync();
        }
    }
}
