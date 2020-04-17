using HDFC.Core.Dtos.Procurements;
using HDFC.Core.Enums;
using HDFC.Core.Interfaces;
using HDFC.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HDFC.Core.Entities.Budgeting
{
    public class Budget : AggregateRoot
    {
        private Budget() { }

        public Budget(string name, string description, long budgetHeadId, long budgetCategoryId, long departmentId, long currencyId, long userId)
        {
            Name = name;
            Description = description;
            BudgetStatus = BudgetStatusEnum.Active;
            BudgetHeadId = budgetHeadId;
            BudgetCategoryId = budgetCategoryId;
            DepartmentId = departmentId;
            CurrencyId = currencyId;
            UpdateAudit(userId);
        }

        public void SetBudgetAmountData(DateTime startDate, DateTime endDate, DateTime transactionDate, decimal basicAmount, decimal taxAmount)
        {
            StartDate = startDate;
            EndDate = endDate;
            TransactionDate = transactionDate;
            BasicAmount = basicAmount;
            TaxAmount = taxAmount;
        }

        public void SetBudgetData(string budgetType, string referenceId, long divisionId, long subDivisionId, long personInChargeId, string justification)
        {
            BudgetType = budgetType;
            ReferenceId = referenceId;
            DivisionId = divisionId;
            SubDivisionId = subDivisionId;
            PersonInChargeId = personInChargeId;
            Justification = justification;
        }

        public void CalculateTotalAmount()
        {
            TotalAmount = BasicAmount + TaxAmount;
        }

        public void AddSubBudgets(List<SubBudget> subBudgets, long userId)
        {
            SubBudgets = new List<SubBudget>();
            foreach (var item in subBudgets)
            {
                var subBudget = new SubBudget(item.StartDate, item.EndDate, item.TransactionDate, item.BudgetAmount, item.TransactionType);
                SubBudgets.Add(subBudget);
            }
            UpdateAudit(userId);
        }
        public void AddSpendLimits(List<BudgetSpendLimit> budgetSpendLimits, long userId)
        {
            BudgetSpendLimits = new List<BudgetSpendLimit>();
            foreach (var item in budgetSpendLimits)
            {
                var budgetSpendLimit = new BudgetSpendLimit(item.StartDate, item.EndDate, item.SpendLimit, item.Remarks);
                BudgetSpendLimits.Add(budgetSpendLimit);
            }
            UpdateAudit(userId);
        }
        public void AddCostCodes(List<BudgetCostCode> budgetCostCode, long userId)
        {
            BudgetCostCodes = new List<BudgetCostCode>();
            foreach (var item in budgetCostCode)
            {
                var budgetSpendLimit = new BudgetCostCode(item.CostCodeId);
                BudgetCostCodes.Add(budgetSpendLimit);
            }
            UpdateAudit(userId);
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public BudgetStatusEnum BudgetStatus { get; private set; }
        public long BudgetHeadId { get; private set; }
        public long BudgetCategoryId { get; private set; }
        public long DepartmentId { get; private set; }
        public long CurrencyId { get; private set; }
        public string BudgetType { get; private set; }
        public string ReferenceId { get; private set; }
        public long? BaseId { get; private set; }
        public int RevisionNumber { get; private set; }
        public bool IsCurrent { get; private set; }
        public long DivisionId { get; private set; }
        public long SubDivisionId { get; private set; }
        public long PersonInChargeId { get; private set; }
        public string Justification { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public decimal BasicAmount { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal TotalAmount { get; private set; }

        public IList<BudgetCostCode> BudgetCostCodes { get; private set; }
        public IList<SubBudget> SubBudgets { get; private set; }
        public IList<BudgetSpendLimit> BudgetSpendLimits { get; private set; }

        public void SetInActive()
        {
            IsCurrent = false;
        }
        public void SetActive()
        {
            IsCurrent = true;
        }
        public void SetRevisionNumber(int revisionNumber)
        {
            RevisionNumber = revisionNumber;
        }
        public void SetBaseId(long? baseId)
        {
            BaseId = baseId;
        }

        public void RemoveSubBudgets(int subBudgetId, long userId)
        {
            var item = SubBudgets.Single(s => s.Id == subBudgetId);
            SubBudgets.Remove(item);
            UpdateAudit(userId);
        }

        public void RemoveBudgetSpendLimits(int budgetSpendLimit, long userId)
        {
            var budgetSpendLimits = BudgetSpendLimits.Single(s => s.Id == budgetSpendLimit);
            BudgetSpendLimits.Remove(budgetSpendLimits);
            UpdateAudit(userId);
        }

        public void Hold(long userId)
        {
            BudgetStatus = BudgetStatusEnum.Hold;
            UpdateAudit(userId);
        }
    }
}
