using HDFC.Core.Enums;
using HDFC.Core.Interfaces;
using HDFC.Core.SharedKernel;
using System;
using System.Collections.Generic;

namespace HDFC.Core.Entities.Masters.Vendor
{
    public class Vendor : AggregateRoot, IStatus
    {
        private Vendor()
        {

        }
        public Vendor(string name, VendorTypeEnum vendorType, long userId)
        {
            //Invitation
            Name = name;
            ContactPerson = name;
            VendorStatus = VendorStatuEnum.Invited;
            UpdateAudit(userId);
        }
        public Vendor(string name, string contactPerson, bool isCompositeTaxable, StatusEnum status, string proprietorName, long userId)
        {
            Name = name;
            ContactPerson = contactPerson;
            IsCompositeTaxable = isCompositeTaxable;
            Status = status;
            ProprietorName = proprietorName;
            UpdateAudit(userId);
        }
        public void SetContactDetailsAndIdentity(string mobileNo, string landlineNo, string panNo, string panHolderName, string aadharNo)
        {

            MobileNo = mobileNo;
            LandlineNo = landlineNo;
            PANNo = panNo;
            PANHolderName = panHolderName;
            AadharNo = aadharNo;
        }

        public void SetVendorTypeAndStatus(VendorTypeEnum vendorType, VendorStatuEnum vendorStatus)
        {
            VendorType = vendorType;
            VendorStatus = vendorStatus;
        }


        public string Name { get; private set; }
        public string ContactPerson { get; private set; }
        public VendorTypeEnum VendorType { get; private set; }
        public string MobileNo { get; private set; }
        public string LandlineNo { get; private set; }
        public string PANNo { get; private set; }
        public string PANHolderName { get; private set; }
        public string AadharNo { get; private set; }
        public string CompanyType { get; private set; }
        public string BusinessNature { get; private set; }
        public string ProprietorName { get; private set; }
        public bool IsCompositeTaxable { get; private set; }
        public bool IsRegisteredUnderMSME { get; private set; }
        public StatusEnum Status { get; private set; }
        public VendorStatuEnum VendorStatus { get; private set; }

        public IList<VendorAttachment> VendorAttachments { get; private set; }
        public IList<VendorUser> VendorUsers { get; private set; }
        public IList<VendorMSMEDetail> VendorMSMEDetails { get; private set; }
        public IList<VendorBankDetail> VendorBankDetails { get; private set; }
        public IList<VendorLocation> VendorLocations { get; private set; }


        public void Update(string name, string contactPerson, bool isCompositeTaxable, StatusEnum status, string proprietorName, long userId)
        {
            Name = name;
            ContactPerson = contactPerson;
            IsCompositeTaxable = isCompositeTaxable;
            Status = status;
            ProprietorName = proprietorName;
            UpdateAudit(userId);
        }

        public void AddVendorUsers(List<VendorUser> vendorUsers, long userId)
        {
            VendorUsers = new List<VendorUser>();
            foreach (var item in vendorUsers)
            {
                var vendorUser = new VendorUser(item.Email, item.PhoneNumber, item.Designation);
                VendorUsers.Add(vendorUser);
            }
            UpdateAudit(userId);
        }
    }


}
