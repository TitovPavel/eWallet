using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class User : IdentityUser
    {
        public List<Expense> Expenses { get; set; }
        public List<Income> Incomes { get; set; }
    }
}
