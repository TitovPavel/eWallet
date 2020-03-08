using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class BalanceViewModel
    {
        public CreateExpenseViewModel CreateExpense { get; set; }
        public CreateIncomeViewModel CreateIncome { get; set; }
        public List<OperationViewModel> Operations { get; set; }
        public string SearchString { get; set; }
        public SortOperationsViewModel SortOperations { get; set; }
    }
}
