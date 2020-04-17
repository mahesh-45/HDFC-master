using System;

namespace HDFC.Core.Dtos.Procurements.Budgeting
{
    public class SubBudgetDto : BaseEntityDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal BudgetAmount { get; set; }
        public string TransactionType { get; set; }
    }
}
