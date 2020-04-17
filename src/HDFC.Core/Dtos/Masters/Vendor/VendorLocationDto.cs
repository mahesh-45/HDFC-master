using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Dtos.Masters.Vendor
{
    public class VendorLocationDto : BaseEntityDto
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get;  set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string TaxCode { get; set; }
        public int LocationTypeId { get;  set; }   //master
        public int? RegionId { get; set; }
    }
}
