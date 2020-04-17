using HDFC.Core.Enums;
using System.Collections.Generic;

namespace HDFC.Core.Dtos.Masters
{
    public class BudgetHeadDto : AggregateRootDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int? ParentId { get; set; }
        public StatusEnum Status { get; set; }
        public int? DepartmentId { get; set; }
        public int? CostCodeId { get; set; }
        public List<BudgetHeadDto> ChildBudgetHead { get; set; }
    }

    public class BudgetHeadListDto
    {
        public List<BudgetHeadDto> Items { get; set; }
        public int Total_count { get; set; }

    }
}
