using System.Collections.Generic;

namespace HDFC.Core.Dtos.Masters.Employee
{
    public class EmployeeListDto
    {
        public List<EmployeeDto> Items { get; set; }
        public int Total_count { get; set; }

    }
}
