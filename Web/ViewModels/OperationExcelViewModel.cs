using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class OperationExcelViewModel
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Sum { get; set; }
        public string SumByn { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
    }
}
