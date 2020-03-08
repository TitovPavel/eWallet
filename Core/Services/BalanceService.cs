using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IAsyncRepository<Income> _incomeRepository;
        private readonly IAsyncRepository<Expense> _expenseRepository;
        private readonly IAsyncRepository<IncomeCategory> _incomeCategoryRepository;
        private readonly IAsyncRepository<ExpenseCategory> _expenseCategoryRepository;
        private readonly IAsyncRepository<Currency> _currencyRepository;

        public BalanceService(IAsyncRepository<Income> incomeRepository,
            IAsyncRepository<Expense> expenseRepository,
            IAsyncRepository<IncomeCategory> incomeCategoryRepository,
            IAsyncRepository<ExpenseCategory> expenseCategoryRepository,
            IAsyncRepository<Currency> currencyRepository)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _incomeCategoryRepository = incomeCategoryRepository;
            _expenseCategoryRepository = expenseCategoryRepository;
            _currencyRepository = currencyRepository;
        }

        public async Task<decimal> GetBalance(string userId, DateTime dateTime)
        {

            var filterIncomeSpecification = new IncomeSpevification(userId, dateTime);
            var filterExpenseSpecification = new ExpenseSpevification(userId, dateTime);

            IReadOnlyList<Income> incomes = await _incomeRepository.ListAsync(filterIncomeSpecification);
            IReadOnlyList<Expense> expense = await _expenseRepository.ListAsync(filterExpenseSpecification);

            return incomes.Sum(x => x.SumByn) - expense.Sum(x => x.SumByn);
        }

        public async Task<IReadOnlyList<Currency>> GetCurrency()
        {

            return await _currencyRepository.ListAllAsync();
            
        }

        public async Task<List<IncomeCategory>> GetUserIncomeCategories(string userId)
        {

            var filterIncomeCategorySpecification = new IncomeCategorySpevification(userId);
            
            List<IncomeCategory> incomeCategory = await _incomeCategoryRepository.ListAsync(filterIncomeCategorySpecification) as List<IncomeCategory>;
            
            return incomeCategory;

        }

        public async Task<List<ExpenseCategory>> GetUserExpenseCategories(string userId)
        {

            var filterExpenseCategorySpecification = new ExpenseCategorySpevification(userId);

            List<ExpenseCategory> expenseCategory = await _expenseCategoryRepository.ListAsync(filterExpenseCategorySpecification) as List<ExpenseCategory>;


            return expenseCategory;

        }

        public async Task<IReadOnlyList<Income>> GetUserIncomes(string userId, string searchString)
        {

            var filterIncomeSpecification = new IncomeSpevification(userId, searchString);
            var filterExpenseSpecification = new ExpenseSpevification(userId, searchString);

            IReadOnlyList<Income> incomes = await _incomeRepository.ListAsync(filterIncomeSpecification);
            IReadOnlyList<Expense> expense = await _expenseRepository.ListAsync(filterExpenseSpecification);

            return incomes;

        }

        public async Task<IReadOnlyList<Expense>> GetUserExpenses(string userId, string searchString)
        {

            var filterExpenseSpecification = new ExpenseSpevification(userId, searchString);

            IReadOnlyList<Expense> expense = await _expenseRepository.ListAsync(filterExpenseSpecification);

            return expense;

        }

        public async Task AddIncome(Income income)
        {

            await _incomeRepository.AddAsync(income);

        }

        public async Task AddExpense(Expense expense)
        {

            await _expenseRepository.AddAsync(expense);

        }

        public async Task AddIncomeCategory(IncomeCategory incomeCategory)
        {

            await _incomeCategoryRepository.AddAsync(incomeCategory);

        }

        public async Task AddExpenseCategory(ExpenseCategory expenseCategory)
        {

            await _expenseCategoryRepository.AddAsync(expenseCategory);

        }
    }
}
