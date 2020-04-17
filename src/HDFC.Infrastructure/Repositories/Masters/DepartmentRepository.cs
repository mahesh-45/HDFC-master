using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
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
    class DepartmentRepository : ActiveRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AnyAsync(Department department)
        {
            if (department.Id > 0)
            {
                return await _dbContext.Departments.AnyAsync(c => c.Id != department.Id && c.Name == department.Name && c.Code == department.Code);
            }
            else
            {
                return await _dbContext.Departments.AnyAsync(c => c.Name == department.Name && c.Code == department.Code);
            }
        }

        public async Task<DepartmentListDto> GetList(string search)
        {

            DepartmentListDto res = new DepartmentListDto();

            List<DepartmentDto> departments = await (from a in _dbContext.Departments
                                                  where (search != null ? (a.Name.Contains(search)
                                                 || a.Code.Contains(search)) : true)
                                                  select new DepartmentDto()
                                                  {
                                                      Code = a.Code,
                                                      Name = a.Name,
                                                      Status = a.Status,
                                                      CreatedDate = a.CreatedDate,
                                                      Id = a.Id,
                                                      Uid = a.Uid

                                                  }).ToListAsync();
            res.Total_count = departments.Count();
            res.Items = departments;

            return res;
        }
    }
}
