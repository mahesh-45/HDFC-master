using HDFC.Core.Dtos.Procurements.Budgeting;
using HDFC.Core.Enums;
using System;
using System.Collections.Generic;

namespace HDFC.Core.Dtos.Procurements
{
    public class BudgetDto : AggregateRootDto
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public BudgetStatusEnum BudgetStatus { get; set; }

        public long BudgetHeadId { get; set; }

        public long BudgetCategoryId { get; set; }

        public long DepartmentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal BasicAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public long CurrencyId { get; set; }

        public string BudgetType { get; set; }

        public long DivisionId { get; set; }

        public long SubDivisionId { get; set; }

        public long PersonInChargeId { get; set; }

        public string Justification { get; set; }

        public string ReferenceId { get; set; }

        public List<SubBudgetDto> SubBudgets { get; set; }
        public List<BudgetCostCodeDto> BudgetCostCodes { get; set; }
        public List<BudgetSpendLimitDto> BudgetSpendLimits { get; set; }
    }



}
