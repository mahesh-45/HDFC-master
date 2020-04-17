using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Dtos.Procurements.Budgeting
{
    public class BudgetListDto
    {
        public List<BudgetDto> Items { get; set; }
        public int Total_count { get; set; }

    }
}
