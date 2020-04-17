using System;
using HDFC.Core.Enums;
using HDFC.Core.SharedKernel;

namespace HDFC.Core.Entities.Masters.Vendor
{
    public class VendorBankDetail : BaseEntity
    {
        private VendorBankDetail() { }

        public VendorBankDetail(string accountHolderName, string bankName, string bankCode, string accountNumber, AccountTypeEnum accountType)
        {
            AccountHolderName = accountHolderName;
            BankName = bankName;
            BankCode = bankCode;
            AccountNumber = accountNumber;
            AccountType = accountType;
        }

        public string AccountHolderName { get; private set; }
        public string BankName { get; private set; }
        public string BankCode { get; private set; }
        public string AccountNumber { get; private set; }
        public AccountTypeEnum AccountType { get; private set; }

        public long VendorId { get; private set; }
        public Vendor Vendor { get; private set; }

  

      

        public void Update(string accountHolderName, string bankName, string bankCode, string accountNumber, AccountTypeEnum accountType)
        {
            
            AccountHolderName = accountHolderName;
            BankName = bankName;
            BankCode = bankCode;
            AccountNumber = accountNumber;
            AccountType = accountType;
        }
    }
}
