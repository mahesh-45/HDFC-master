using HDFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Dtos.Masters
{
    public class NumberingSequenceDto : AggregateRootDto
    {
        public string Prefix { get; set; }
        public string Number { get; set; }
        public StatusEnum Status { get; set; }
        public NumberingSequenceTypeEnum NumberingSequenceType { get; set; }
    }
    public class NumberingSequenceListDto
    {
        public List<NumberingSequenceDto> Items { get; set; }
        public int Total_count { get; set; }

    }
}
