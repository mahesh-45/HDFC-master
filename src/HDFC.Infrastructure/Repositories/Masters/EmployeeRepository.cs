using System;
using System.Collections.Generic;
using System.Text;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Repositories.Masters;
using HDFC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using HDFC.Core.Entities.Masters.Employee;
using HDFC.Core.Dtos.Masters.Employee;

namespace HDFC.Infrastructure.Repositories.Masters
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {


        public EmployeeRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Employee>> ActiveToListAsync()
        {
            return await _dbContext.Set<Employee>().Where(s => s.EmployeeStatus == Core.Enums.EmployeeStatusEnum.Active).ToListAsync();
        }

        public async Task<bool> AnyAsync(Employee employee)
        {
            if (employee.Id > 0)
            {
                return await _dbContext.Employees.AnyAsync(c => c.Id != employee.Id && c.EmployeeNumber == employee.EmployeeNumber && c.OfficialEmail == employee.OfficialEmail);
            }
            else
            {
                return await _dbContext.Employees.AnyAsync(c => c.EmployeeNumber == employee.EmployeeNumber && c.OfficialEmail == employee.OfficialEmail);
            }
        }

        public async Task<Employee> GetEmployeeByUserId(long userId)
        {
            return await _dbContext.Employees.Where(c => c.UserId == userId && c.EmployeeStatus == Core.Enums.EmployeeStatusEnum.Active).SingleOrDefaultAsync();
        }

        public async Task<EmployeeListDto> GetList(string search)
        {

            EmployeeListDto res = new EmployeeListDto();

            List<EmployeeDto> employees = await (from a in _dbContext.Employees
                                                 where (search != "null" ? (a.FirstName.Contains(search) || a.EmployeeNumber.Contains(search) || a.OfficialEmail.Contains(search)) : true)
                                                 select new EmployeeDto()
                                                 {
                                                     FirstName = a.FirstName,
                                                     MiddleName = a.MiddleName,
                                                     LastName = a.LastName,
                                                     Title = a.Title,
                                                     EmployeeNumber = a.EmployeeNumber,
                                                     Gender = a.Gender,
                                                     PanNumber = a.PanNumber,
                                                     DateOfBirth = a.DateOfBirth,
                                                     MaritalStatus = a.MaritalStatus,
                                                     OfficialEmail = a.OfficialEmail,
                                                     Contact1 = a.Contact1,
                                                     Contact2 = a.Contact2,
                                                     Band = a.Band,
                                                     LocationName = a.LocationName,
                                                     SupervisorEmpCode = a.SupervisorEmpCode,
                                                     HireDate = a.HireDate,
                                                     RecordStatus = a.RecordStatus,
                                                     CountryId = a.CountryId,
                                                     City = a.City,
                                                     Region = a.Region,
                                                     ActualTerminationDate = a.ActualTerminationDate,
                                                     EmployeeStatus = a.EmployeeStatus,
                                                     CreatedDate = a.CreatedDate,
                                                     Id = a.Id,
                                                     Uid = a.Uid

                                                 }).ToListAsync();
            res.Total_count = employees.Count();
            res.Items = employees;

            return res;

        }
    }
}
