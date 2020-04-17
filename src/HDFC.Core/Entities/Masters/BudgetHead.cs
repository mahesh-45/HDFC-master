using HDFC.Core.Enums;
using HDFC.Core.Interfaces;
using HDFC.Core.SharedKernel;

namespace HDFC.Core.Entities.Masters
{
    public class BudgetHead : AggregateRoot, IStatus
    {
        private BudgetHead() { }

        public string Name { get; private set; }
        public string Code { get; private set; }
        public int? ParentId { get; private set; }
        public StatusEnum Status { get; private set; }
        public int? DepartmentId { get; private set; }
        public int? CostCodeId { get; private set; }

        public BudgetHead(string name, string code, int? parentId, StatusEnum status, int? departmentId, int? costCodeId, long userId)
        {
            Name = name;
            Code = code;
            ParentId = parentId == 0 ? null : parentId;
            Status = status;
            DepartmentId = departmentId == 0 ? null : departmentId;
            CostCodeId = costCodeId == 0 ? null : costCodeId;
            UpdateAudit(userId);
        }

        public void Update(string name, string code, int? parentId, StatusEnum status, int? departmentId, int? costCodeId, long userId)
        {
            Name = name;
            Code = code;
            ParentId = parentId == 0 ? null : parentId;
            Status = status;
            DepartmentId = departmentId == 0 ? null : departmentId;
            CostCodeId = costCodeId == 0 ? null : costCodeId;
            UpdateAudit(userId);
        }

    }
}
