using System;
using System.Collections.Generic;
using System.Text;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using System.Threading.Tasks;
using HDFC.Core.Entities.Masters.Employee;
using HDFC.Core.Dtos.Masters.Employee;
using HDFC.Core.SharedKernel;

namespace HDFC.Core.Repositories.Masters
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<bool> AnyAsync(Employee employee);
        Task<EmployeeListDto> GetList(string search);
        Task<List<Employee>> ActiveToListAsync();
        Task<Employee> GetEmployeeByUserId(long userId);
    }
}
