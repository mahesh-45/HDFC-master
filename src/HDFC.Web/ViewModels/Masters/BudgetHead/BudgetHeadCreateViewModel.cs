using HDFC.Core.Dtos;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HDFC.Web.ViewModels.Masters.BudgetHead
{
    public class BudgetHeadCreateViewModel : AggregateRootDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public int? ParentId { get; set; }
        [Required]
        public StatusEnum Status { get; set; }
        public int? DepartmentId { get; set; }
        public int? CostCodeId { get; set; }
        public List<BudgetHeadDto> ChildBudgetHead { get; set; }

    }
}
