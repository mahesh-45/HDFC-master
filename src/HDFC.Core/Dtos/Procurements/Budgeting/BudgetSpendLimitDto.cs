using System;

namespace HDFC.Core.Dtos.Procurements.Budgeting
{

    public class BudgetSpendLimitDto : BaseEntityDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal SpendLimit { get; set; }
        public string Remarks { get; set; }

    }
}
