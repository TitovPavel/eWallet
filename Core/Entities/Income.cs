using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Income : Operation
    {
        public int? CategoryId { get; set; }
        public IncomeCategory Category { get; set; }    
    }
}
