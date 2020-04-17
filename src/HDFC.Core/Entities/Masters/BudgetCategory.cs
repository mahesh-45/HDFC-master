using HDFC.Core.Enums;
using HDFC.Core.Interfaces;
using HDFC.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Entities.Masters
{
    public class BudgetCategory : AggregateRoot, IStatus
    {
        private BudgetCategory() { }

        public BudgetCategory(string name, string code, StatusEnum status)
        {
            Name = name;
            Code = code;
            Status = status;
        }
        public BudgetCategory(string name, string code, StatusEnum status, long userId)
        {
            Name = name;
            Code = code;
            Status = status;
            UpdateAudit(userId);
        }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public StatusEnum Status { get; private set; }

        public void Update(string name, string code, StatusEnum status, long userId)
        {
            Name = name;
            Code = code;
            Status = status;
        }

    }
}
