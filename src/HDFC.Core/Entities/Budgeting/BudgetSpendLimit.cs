using HDFC.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Entities.Budgeting
{
    public class BudgetSpendLimit : BaseEntity
    {
        private BudgetSpendLimit() { }

        public BudgetSpendLimit(DateTime startDate, DateTime endDate, decimal spendLimit, string remarks)
        {
            StartDate = startDate;
            EndDate = endDate;
            SpendLimit = spendLimit;
            Remarks = remarks;
        }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal SpendLimit { get; private set; }
        public string Remarks { get; private set; }
        public long BudgetId { get; private set; }
        public Budget Budget { get; private set; }
    }
}
