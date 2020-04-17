using HDFC.Core.Dtos.Common;
using HDFC.Core.Dtos.Masters.Division;
using HDFC.Core.Entities.Masters.Division;
using HDFC.Core.Repositories.Masters;
using HDFC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDFC.Infrastructure.Repositories.Masters
{
    public class DivisionRepository : ActiveRepository<Division>, IDivisionRepository
    {
        public DivisionRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> AnyAsync(Division division)
        {
            if (division.Id > 0)
            {
                return await _dbContext.Divisions.AnyAsync(c => c.Id != division.Id && c.Code == division.Code);
            }
            else
            {
                return await _dbContext.Divisions.AnyAsync(c => c.Code == division.Code);
            }
        }

        public async Task<Division> GetDivision(string uid)
        {
            return await _dbContext.Divisions.Include(x => x.SubDivisions).Where(x => x.Uid == uid).SingleOrDefaultAsync();
        }

        public async Task<DivisionListDto> GetList(SearchDto searchDto)
        {
            DivisionListDto res = new DivisionListDto();

            var SkipPage = searchDto.Page * searchDto.PageSize;

            List<DivisionDto> division = await(from a in _dbContext.Divisions
                                                           where (searchDto.Search != null ? (a.Name.Contains(searchDto.Search) || a.Code.Contains(searchDto.Search)) : true)
                                                           select new DivisionDto()
                                                           {
                                                               Name = a.Name,
                                                               Code = a.Code,
                                                               Status = a.Status,
                                                               Id = a.Id,
                                                               Uid = a.Uid,
                                                               CreatedDate = a.CreatedDate
                                                           }).ToListAsync();
            res.Total_count = division.Count();

            switch (searchDto.Sort + "_" + searchDto.Order)
            {
                case "name_desc":
                    res.Items = division.Skip(SkipPage).Take(searchDto.PageSize).OrderByDescending(s => s.Name).ToList();
                    break;
                case "name_asc":
                    res.Items = division.Skip(SkipPage).Take(searchDto.PageSize).OrderBy(s => s.Name).ToList();
                    break;
                case "code_desc":
                    res.Items = division.Skip(SkipPage).Take(searchDto.PageSize).OrderByDescending(s => s.Code).ToList();
                    break;
                case "code_asc":
                    res.Items = division.Skip(SkipPage).Take(searchDto.PageSize).OrderBy(s => s.Code).ToList();
                    break;
                case "createdDate_desc":
                    res.Items = division.Skip(SkipPage).Take(searchDto.PageSize).OrderByDescending(s => s.CreatedDate).ToList();
                    break;
                case "createdDate_asc":
                    res.Items = division.Skip(SkipPage).Take(searchDto.PageSize).OrderBy(s => s.CreatedDate).ToList();
                    break;
            }

            return res;
        }
    }
}
