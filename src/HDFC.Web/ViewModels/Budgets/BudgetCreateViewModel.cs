using HDFC.Core.Dtos;
using HDFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HDFC.Web.ViewModels.Budgets
{
    public class BudgetCreateViewModel 
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string BudgetStatus { get; set; }
        [Required]
        public long BudgetHeadId { get; set; }
        [Required]
        public long BudgetCategoryId { get; set; }
        [Required]
        public long DepartmentId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public decimal BasicAmount { get; set; }
        [Required]
        public decimal TaxAmount { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public long CurrencyId { get; set; }
        [Required]
        public string BudgetType { get; set; }
        [Required]
        public long DivisionId { get; set; }
        [Required]
        public long SubDivisionId { get; set; }
        [Required]
        public long PersonInChargeId { get; set; }
        [Required]
        public string Justification { get; set; }

        public List<BudgetCostCodeViewModel> BudgetCostCodes { get; set; }
        public List<BudgetSpendLimitViewModel> BudgetSpendLimits { get; set; }
    }
}

public class SubBudgetViewModel
{
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public DateTime TransactionDate { get; set; }
    [Required]
    public decimal BudgetAmount { get; set; }
    [Required]
    public string TransactionType { get; set; }
}

public class BudgetSpendLimitViewModel
{
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public decimal SpendLimit { get; set; }

    [Required]
    public string Remarks { get; set; }
}

public class BudgetCostCodeViewModel
{
    [Required]
    public long CostCodeId { get; set; }

}

