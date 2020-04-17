using HDFC.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Entities.Masters.Division
{
    public class SubDivision : BaseEntity
    {
        private SubDivision()
        {
        }

        public string Name { get; private set; }
        public string Code { get; private set; }

        public long DivisionId { get; private set; }
        public Division Division { get; private set; }
        public SubDivision(string name, string code)
        {
            Name = name;
            Code = code;
        }

      
    }
}
