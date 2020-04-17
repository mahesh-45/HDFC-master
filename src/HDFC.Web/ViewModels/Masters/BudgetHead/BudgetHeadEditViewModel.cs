using HDFC.Core.Dtos;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HDFC.Web.ViewModels.Masters.BudgetHead
{
    public class BudgetHeadEditViewModel : BudgetHeadCreateViewModel
    {
        [Required]
        public string Uid { get; set; }
       
    }
}
