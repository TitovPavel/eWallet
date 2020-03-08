using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBalanceService
    {
        Task<decimal> GetBalance(string userId, DateTime dateTime);
        Task<IReadOnlyList<Currency>> GetCurrency();
        Task<List<IncomeCategory>> GetUserIncomeCategories(string userId);
        Task<List<ExpenseCategory>> GetUserExpenseCategories(string userId);
        Task<IReadOnlyList<Income>> GetUserIncomes(string userId, string searchString);
        Task<IReadOnlyList<Expense>> GetUserExpenses(string userId, string searchString);
        Task AddIncome(Income income);
        Task AddExpense(Expense expense);
        Task AddIncomeCategory(IncomeCategory incomeCategory);
        Task AddExpenseCategory(ExpenseCategory expenseCategory);
    }
}
