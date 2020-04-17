using System;
using HDFC.Core.Enums;
using HDFC.Core.SharedKernel;

namespace HDFC.Core.Entities.Masters.Vendor
{
    public class VendorMSMEDetail : BaseEntity
    {
        private VendorMSMEDetail() { }

        public VendorMSMEDetail(string msmeRegistrationNumber, DateTime dateOfMSMERegistration, EnterpriseEnum enterpriseUnderMSME)
        {

            MSMERegistrationNumber = msmeRegistrationNumber;
            DateofMSMERegistration = dateOfMSMERegistration;
            EnterpriseUnderMSME = enterpriseUnderMSME;
        }
        public string MSMERegistrationNumber { get; private set; }
        public DateTime? DateofMSMERegistration { get; private set; }
        public EnterpriseEnum EnterpriseUnderMSME { get; private set; }
        public long VendorId { get; private set; }
        public Vendor Vendor { get; private set; }

        public void Update(string msmeRegistrationNumber, DateTime dateOfMSMERegistration, EnterpriseEnum enterpriseUnderMSME)
        {
           
            MSMERegistrationNumber = msmeRegistrationNumber;
            DateofMSMERegistration = dateOfMSMERegistration;
            EnterpriseUnderMSME = enterpriseUnderMSME;
        }
    }
}
