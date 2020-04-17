using HDFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.ViewModels.Masters.CostCodes
{
    public class CostCodeCreateViewModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string BHEmpCode { get; set; }
        [Required]
        public string BH { get; set; }
        [Required]
        public string ADGroup { get; set; }
        [Required]
        public string ADEmpCode { get; set; }
        [Required]
        public string Head { get; set; }
        [Required]
        public StatusEnum Status { get; set; }
    }
}
