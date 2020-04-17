using HDFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.ViewModels.Masters.Division
{
    public class DivisionEditViewModel: DivisionCreateViewModel
    {
        [Required]
        public string Uid { get; set; }
      
    }
  
}
