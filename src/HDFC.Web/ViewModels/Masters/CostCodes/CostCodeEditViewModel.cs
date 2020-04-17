using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.ViewModels.Masters.CostCodes
{
    public class CostCodeEditViewModel: CostCodeCreateViewModel
    {
        [Required]
        public string Uid { get; set; }
    }
}
