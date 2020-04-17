using System;
using HDFC.Core.SharedKernel;

namespace HDFC.Core.Entities.Masters.Vendor
{
    public class VendorUser : BaseEntity
    {
        private VendorUser() { }

        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Designation { get; private set; }
        public long? UserId { get; private set; }
        public long VendorId { get; private set; }
        public Vendor Vendor { get; private set; }

        public VendorUser(string email, string phoneNumber, string designation)
        {
            
            Email = email;
            PhoneNumber = phoneNumber;
            Designation = designation;
           
        }

        public void Update(Vendor vendor, long vendorId, string email, string phoneNumber, string designation, long userId)
        {
            Vendor = vendor;
            VendorId = vendorId;
            Email = email;
            PhoneNumber = phoneNumber;
            Designation = designation;
            UserId = userId;
        }

    }
}
