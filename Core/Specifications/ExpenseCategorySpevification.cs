using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{

    public class ExpenseCategorySpevification : BaseSpecification<ExpenseCategory>
    {
        public ExpenseCategorySpevification(string userId)
            : base(o => o.UserId == userId)
        {

        }
    }
}
