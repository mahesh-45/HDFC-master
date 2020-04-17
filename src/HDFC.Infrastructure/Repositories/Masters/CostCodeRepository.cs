using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Repositories.Masters;
using HDFC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Infrastructure.Repositories.Masters
{
    class CostCodeRepository : ActiveRepository<CostCode>, ICostCodeRepository
    {
        public CostCodeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AnyAsync(CostCode costCode)
        {
            if (costCode.Id > 0)
            {
                return await _dbContext.CostCodes.AnyAsync(c => c.Id != costCode.Id && (c.Name == costCode.Name ||c.Code == costCode.Code));
            }
            else
            {
                return await _dbContext.CostCodes.AnyAsync(c => c.Name == costCode.Name && c.Code == costCode.Code);
            }
        }

        public async Task<CostCodeListDto> GetList(string search)
        {
            
            CostCodeListDto res = new CostCodeListDto();

            List<CostCodeDto> costCodes = await (from a in _dbContext.CostCodes
                                                where (search != null ? (a.Name.Contains(search) 
                                                || a.Code.Contains(search)) : true)
                                                   select new CostCodeDto()
                                                   {
                                                        Code = a.Code,
                                                        Name = a.Name,
                                                        BHEmpCode = a.BHEmpCode,
                                                        BH = a.BH,
                                                        ADGroup = a.ADGroup,
                                                        ADEmpCode = a.ADEmpCode,
                                                        Head = a.Head,
                                                        Status = a.Status,
                                                        CreatedDate = a.CreatedDate,
                                                        Id = a.Id,
                                                        Uid = a.Uid

                                                   }).ToListAsync();
            res.Total_count = costCodes.Count();
            res.Items = costCodes;
            
            return res;
        }
    }
}
