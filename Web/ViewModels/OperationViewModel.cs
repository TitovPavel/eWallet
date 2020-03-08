using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class OperationViewModel
    {
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        public DateTime Date { get; set; }
        public string Sum { get; set; }
        public decimal SumByn { get; set; }
        public string Comment { get; set; }
        public string Category { get; set; }
        public string Currency { get; set; }   
    }
}
