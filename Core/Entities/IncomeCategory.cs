using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class IncomeCategory : BaseEntity
    {
        public string Title { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    } 
}
