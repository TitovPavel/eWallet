using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class CategoryViewModel
    {
        public CreateExpenseCategoryViewModel CreateExpense { get; set; }
        public CreateIncomeCategoryViewModel CreateIncome { get; set; }
        public List<ItemCategoryViewModel> ExpenseCategories { get; set; }
        public List<ItemCategoryViewModel> IncomeCategories { get; set; }
    }
}
