using HDFC.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Entities.Budgeting
{
    public class BudgetCostCode : BaseEntity
    {
        private BudgetCostCode() { }

        public BudgetCostCode(long costCodeId)
        {
            CostCodeId = costCodeId;
        }

        public long CostCodeId { get; private set; }
        public long BudgetId { get; private set; }
        public Budget Budget { get; private set; }
    }
}
