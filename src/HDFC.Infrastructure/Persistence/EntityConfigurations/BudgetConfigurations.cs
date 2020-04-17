using HDFC.Core.Entities.Budgeting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Infrastructure.Persistence.EntityConfigurations
{
    public class BudgetConfigurations : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.HasMany(i => i.SubBudgets).WithOne(s => s.Budget).HasForeignKey(i => i.BudgetId);
            builder.HasMany(i => i.BudgetCostCodes).WithOne(s => s.Budget).HasForeignKey(i => i.BudgetId);
            builder.HasMany(i => i.BudgetSpendLimits).WithOne(s => s.Budget).HasForeignKey(i => i.BudgetId);
        }
    }
}
