using System;
using HDFC.Core.Enums;
using System.Collections.Generic;

namespace HDFC.Core.Dtos.Masters
{

    public class CostCodeDto : AggregateRootDto
    {

        public string Code { get; set; }
        public string Name { get; set; }
        public string BHEmpCode { get; set; }
        public string BH { get; set; }
        public string ADGroup { get; set; }
        public string ADEmpCode { get; set; }
        public string Head { get; set; }                               //Business Head/Group Head
        public StatusEnum Status { get; set; }

    }
    public class CostCodeListDto
    {
        public List<CostCodeDto> Items { get; set; }
        public int Total_count { get; set; }
    }
}

