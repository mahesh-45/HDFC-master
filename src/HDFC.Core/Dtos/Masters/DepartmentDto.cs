using HDFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Dtos.Masters
{
    public class DepartmentDto : AggregateRootDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public StatusEnum Status { get; set; }
    }

    public class DepartmentListDto
    {
        public List<DepartmentDto> Items { get; set; }
        public int Total_count { get; set; }
    }
}
