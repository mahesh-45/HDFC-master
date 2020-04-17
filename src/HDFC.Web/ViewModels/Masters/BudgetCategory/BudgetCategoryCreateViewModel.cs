using HDFC.Core.Dtos;
using HDFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.ViewModels.Masters.BudgetCategory
{
    public class BudgetCategoryCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public StatusEnum Status { get; set; }
    }
}
