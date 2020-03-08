using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{

    public class IncomeCategorySpevification : BaseSpecification<IncomeCategory>
    {
        public IncomeCategorySpevification(string userId)
            : base(o => o.UserId == userId)
        {

        }
    }
}