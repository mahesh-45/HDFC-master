using HDFC.Core.Enums;
using System.Collections.Generic;

namespace HDFC.Core.Dtos.Masters
{
    public class CountryDto : AggregateRootDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public StatusEnum Status { get; set; }
    }

    public class CountryListDto
    {
        public List<CountryDto> Items { get; set; }
        public int Total_count { get; set; }

    }
}
