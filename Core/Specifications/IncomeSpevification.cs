using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{

    public class IncomeSpevification : BaseSpecification<Income>
    {
        public IncomeSpevification(string userId, string searchString)
            : base(o => (o.UserId == userId && (String.IsNullOrEmpty(searchString)
                || o.Category.Title.Contains(searchString)
                || o.Comment.Contains(searchString)
                || o.Currency.Code.Contains(searchString))))
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.Currency);
        }

        public IncomeSpevification(string userId, DateTime dateTime)
            : base(o => (o.UserId == userId && o.Date <= dateTime))
        {

        }
    }
}
