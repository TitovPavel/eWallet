using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Currency : BaseEntity
    {
        public string Code { get; set; }

        public Currency(int id, string code)
        {
            Id = id;
            Code = code;
        }
    }
}
