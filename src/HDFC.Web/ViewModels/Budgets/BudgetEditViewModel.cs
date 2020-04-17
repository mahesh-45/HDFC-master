using HDFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HDFC.Web.ViewModels.Budgets
{
    public class BudgetEditViewModel : BudgetCreateViewModel
    {
        [Required]
        public string Uid { get; set; }
       
    }

}
