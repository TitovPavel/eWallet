using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class CreateIncomeViewModel
    {
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Сумма")]
        [Range(0.01, 1000000000)]
        [DataType(DataType.Currency)]
        public decimal Sum { get; set; }
        [Display(Name = "Сумма BYN")]
        public decimal SumByn { get; set; }
        [Display(Name = "Комментарий")]
        public string Comment { get; set; }
        [Display(Name = "Категория")]
        public int? CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        [Required]
        [Display(Name = "Валюта")]
        public int CurrencyId { get; set; }
        public IEnumerable<SelectListItem> Currencies { get; set; }
        [HiddenInput]
        public decimal Rate { get; set; } = 1;

    }
}
