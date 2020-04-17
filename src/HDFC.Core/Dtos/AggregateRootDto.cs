using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Dtos
{
    public class AggregateRootDto : BaseEntityDto
    {
        public long CreatedBy { get; set; }
        public string Uid { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedByUsername { get; set; }
    }
}
