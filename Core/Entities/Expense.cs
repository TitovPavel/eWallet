using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Expense : Operation
    {
        public int? CategoryId { get; set; }
        public ExpenseCategory Category { get; set; }
    }
}
