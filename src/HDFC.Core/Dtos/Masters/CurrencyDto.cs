using HDFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Dtos.Masters
{
    public class CurrencyDto : AggregateRootDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
    }
    public class CurrencyListDto
    {
        public List<CurrencyDto> Items { get; set; }
        public int Total_count { get; set; }
    }
}
