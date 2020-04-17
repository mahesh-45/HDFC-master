using HDFC.Core.Dtos.Common;
using HDFC.Core.Dtos.Masters.Division;
using HDFC.Core.Entities.Masters.Division;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HDFC.Core.Repositories.Masters
{
    public interface IDivisionRepository : IRepository<Division>
    {
        Task<DivisionListDto> GetList(SearchDto searchDto);
        Task<Division> GetDivision(string uid);
        Task<bool> AnyAsync(Division division);
    }
}
