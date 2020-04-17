using HDFC.Core.Dtos;
using HDFC.Core.Enums;
using HDFC.Core.Dtos.Masters;
using System.ComponentModel.DataAnnotations;
using HDFC.Web.ViewModels.Masters.BudgetCategory;

namespace HDFC.Web.Api.Masters
{
    public class BudgetCategoryEditViewModel : BudgetCategoryCreateViewModel
    {
        [Required]
        public string Uid { get; set; }
       


    }
}
