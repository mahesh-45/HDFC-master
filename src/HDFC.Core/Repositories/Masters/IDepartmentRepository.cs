using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HDFC.Core.Repositories.Masters
{
    public interface IDepartmentRepository : IActiveRepository<Department>
    {
        Task<bool> AnyAsync(Department department);

        Task<DepartmentListDto> GetList(string search);
    }
}
