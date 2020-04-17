using HDFC.Core.Dtos;
using HDFC.Core.Dtos.Masters.Vendor;
using HDFC.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HDFC.Web.ViewModels.Masters.Vendor
{
    public class VendorEditViewModel: VendorCreateViewModel
    {
        [Required]
        public string Uid { get; set; }
      
    }
}
