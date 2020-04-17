using System;
using System.Collections.Generic;
using System.Text;
using HDFC.Core.Enums;

namespace HDFC.Core.Dtos.Masters.Vendor
{
    public class VendorMSMEDetailDto : BaseEntityDto
    {
        public string MSMERegistrationNumber { get; set; }
        public DateTime? DateofMSMERegistration { get; set; }
        public EnterpriseEnum EnterpriseUnderMSME { get; set; }
    }
}
