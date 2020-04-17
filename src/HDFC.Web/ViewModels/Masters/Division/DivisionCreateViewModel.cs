using HDFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.ViewModels.Masters.Division
{
    public class DivisionCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public StatusEnum Status { get; set; }

        public List<SubDivisionViewModel> SubDivisions { get; set; }
    }
    public class SubDivisionViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public StatusEnum Status { get; set; }
    }
}
