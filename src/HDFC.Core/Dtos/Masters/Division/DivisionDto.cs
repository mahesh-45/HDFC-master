using HDFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Dtos.Masters.Division
{
    public class DivisionDto : AggregateRootDto
    {
        public string Name { get;  set; }
        public string Code { get;  set; }
        public StatusEnum Status { get;  set; }

        public List<SubDivisionDto> SubDivisions { get; set; }
    }
    public class DivisionListDto
    {
        public List<DivisionDto> Items { get; set; }
        public int Total_count { get; set; }
    }
}
