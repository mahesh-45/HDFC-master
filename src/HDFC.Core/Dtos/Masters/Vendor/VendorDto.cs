using System;
using System.Collections.Generic;
using System.Text;
using HDFC.Core.Enums;

namespace HDFC.Core.Dtos.Masters.Vendor
{
    public class VendorDto: AggregateRootDto
    {
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Catalogue { get; set; }
        public string SubCatalogue { get; set; }
        public VendorTypeEnum VendorType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string LandlineNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string PANNo { get; set; }
        public string PANHolderName { get; set; }
        public string AadharNo { get; set; }
        public VendorStatuEnum VendorStatus { get; set; }
        public string ProprietorName { get; set; }
        public bool IsCompositeTaxable { get; set; }
        public StatusEnum Status { get; set; }

        public IList<VendorAttachmentDto> VendorAttachments { get; set; }
        public IList<VendorUserDto> VendorUsers { get; set; }
        public IList<VendorMSMEDetailDto> VendorMSMEDetails { get; set; }
        public IList<VendorBankDetailDto> VendorBankDetails { get; set; }
        public IList<VendorLocationDto> VendorLocations { get; set; }
    }

    public class VendorListDto
    {
        public List<VendorDto> Items { get; set; }
        public int Total_count { get; set; }
    }
}
