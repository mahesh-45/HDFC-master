using System;
using System.Collections.Generic;
using System.Text;
using HDFC.Core.Enums;

namespace HDFC.Core.Dtos.Masters.Vendor
{
    public class VendorBankDetailDto: BaseEntityDto
    {
        public string AccountHolderName { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string AccountNumber { get;  set; }
        public AccountTypeEnum AccountType { get;set; }

   
    }
}
