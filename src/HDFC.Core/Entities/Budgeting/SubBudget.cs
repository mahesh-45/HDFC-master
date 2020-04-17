using HDFC.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Entities.Budgeting
{
    public class SubBudget : BaseEntity
    {
        private SubBudget() { }

        public SubBudget(DateTime startDate, DateTime endDate, DateTime transactionDate, decimal budgetAmount, string transactionType)
        {
            StartDate = startDate;
            EndDate = endDate;
            TransactionDate = transactionDate;
            BudgetAmount = budgetAmount;
            TransactionType = transactionType;
        }
        public void Update(DateTime startDate, DateTime endDate, DateTime transactionDate, decimal budgetAmount, string transactionType)
        {
            StartDate = startDate;
            EndDate = endDate;
            TransactionDate = transactionDate;
            BudgetAmount = budgetAmount;
            TransactionType = transactionType;
        }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public decimal BudgetAmount { get; private set; }
        public string TransactionType { get; private set; }
        public long BudgetId { get; private set; }
        public Budget Budget { get; private set; }
    }
}
