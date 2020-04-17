using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Dtos.Masters.Vendor
{
    public class VendorUserDto: BaseEntityDto
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Designation { get; set; }
        public long? UserId { get;  set; }
    }
}
