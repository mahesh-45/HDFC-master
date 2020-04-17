using HDFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Dtos.Masters.Division
{
    public class SubDivisionDto
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public StatusEnum Status { get; private set; }
    }
}
