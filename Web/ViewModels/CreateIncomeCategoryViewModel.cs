﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class CreateIncomeCategoryViewModel
    {

        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }
    }
}
