using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Operation : BaseEntity
    {
        public DateTime Date { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public decimal Sum { get; set; }
        public decimal SumByn { get; set; }
        public string Comment { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

    }
}
