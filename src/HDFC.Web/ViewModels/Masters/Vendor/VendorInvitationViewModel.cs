using HDFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.ViewModels.Masters.Vendor
{
    public class VendorInvitationViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public VendorTypeEnum VendorType { get; set; }
       
        public string Project { get; set; }
        public string ProjectLocationCode { get; set; }
        public string VendorOrganisationType { get; set; }

    }
}
