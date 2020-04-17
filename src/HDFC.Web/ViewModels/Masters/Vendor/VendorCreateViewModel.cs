using HDFC.Core.Dtos;
using System;
using System.Collections.Generic;
using HDFC.Core.Dtos.Masters.Vendor;
using HDFC.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace HDFC.Web.ViewModels.Masters.Vendor
{
    public class VendorCreateViewModel : AggregateRootDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ContactPerson { get; set; }
        [Required]
        public string Catalogue { get; set; }
        [Required]
        public string SubCatalogue { get; set; }
        [Required]
        public VendorTypeEnum VendorType { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string MobileNo { get; set; }

        public string LandlineNo { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PinCode { get; set; }
        [Required]
        public string PANNo { get; set; }
        [Required]
        public string PANHolderName { get; set; }
        [Required]
        public string AadharNo { get; set; }
        [Required]
        public VendorStatuEnum VendorStatus { get; set; }
        [Required]
        public string ProprietorName { get; set; }
        [Required]
        public bool IsCompositeTaxable { get; set; }
        [Required]
        public StatusEnum Status { get; set; }

        public IList<VendorAttachmentViewModel> VendorAttachments { get; set; }
        public IList<VendorUserViewModel> VendorUsers { get; set; }
        public IList<VendorMSMEDetailViewModel> VendorMSMEDetails { get; set; }
        public IList<VendorBankDetailViewModel> VendorBankDetails { get; set; }
        public IList<VendorLocationViewModel> VendorLocations { get; set; }
    }

    public class VendorAttachmentViewModel : BaseEntityDto
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public decimal FileSize { get; set; }
        public DateTime UploadedDate { get; set; }
    }

    public class VendorUserViewModel : BaseEntityDto
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Designation { get; set; }
        public long? UserId { get; set; }
    }

    public class VendorMSMEDetailViewModel : BaseEntityDto
    {
        public string MSMERegistrationNumber { get; set; }
        public DateTime? DateofMSMERegistration { get; set; }
        public EnterpriseEnum EnterpriseUnderMSME { get; set; }
    }

    public class VendorBankDetailViewModel : BaseEntityDto
    {
        public string AccountHolderName { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string AccountNumber { get; set; }
        public AccountTypeEnum AccountType { get; set; }
    }

    public class VendorLocationViewModel : BaseEntityDto
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string TaxCode { get; set; }
        public int LocationTypeId { get; set; } 
        public int? RegionId { get; set; }
    }
}
