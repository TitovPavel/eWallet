using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class RateShort
    {
        public int Cur_ID { get; set; }
        public System.DateTime Date { get; set; }
        public decimal? Cur_OfficialRate { get; set; }
        public int Cur_Scale { get; set; }

    }
}
