using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class BalanceController : Controller
    {
        private readonly IBalanceService _balanceService;
        private readonly IRateService _rateService;
        private readonly IMapper _mapper;
        private readonly ISpreadsheetDocument _spreadsheetDocument;
        private readonly UserManager<User> _userManager;

        public BalanceController(IBalanceService balanceService,
            IRateService rateService,
            IMapper mapper,
            UserManager<User> userManager,
            ISpreadsheetDocument spreadsheetDocument)
        {
            _balanceService = balanceService;
            _rateService = rateService;
            _mapper = mapper;
            _userManager = userManager;
            _spreadsheetDocument = spreadsheetDocument;
        }

        [HttpGet]
        [Route("")]
        [Route("Balance/CreateIncome")]
        [Route("Balance/CreateExpense")]
        public async Task<IActionResult> Index(string searchString, SortState sortOrder = SortState.DateTimeDesc)
        {

            string userId = _userManager.GetUserId(User);

            List<IncomeCategory> incomeCategory = await _balanceService.GetUserIncomeCategories(userId);
            List<ExpenseCategory> expenseCategory = await _balanceService.GetUserExpenseCategories(userId);
            IEnumerable<Currency> currency = await _balanceService.GetCurrency();
            SelectList currencySelectList = new SelectList(currency, "Id", "Code");

            CreateIncomeViewModel createIncomeViewModel = new CreateIncomeViewModel
            {
                Categories = new SelectList(incomeCategory, "Id", "Title"),
                Currencies = currencySelectList
            };

            CreateExpenseViewModel createExpenseViewModel = new CreateExpenseViewModel
            {
                Categories = new SelectList(expenseCategory, "Id", "Title"),
                Currencies = currencySelectList
            };
         
            IReadOnlyList<Income> incomes = await _balanceService.GetUserIncomes(userId, searchString);
            IReadOnlyList<Expense> expense = await _balanceService.GetUserExpenses(userId, searchString);

            var operations = _mapper.Map<IReadOnlyList<Income>, List<OperationViewModel>>(incomes);
            operations.AddRange(_mapper.Map<IReadOnlyList<Expense>, List<OperationViewModel>>(expense));

            operations = sortOrder switch
            {
                SortState.DateTimeDesc => operations.OrderByDescending(s => s.Date).ToList(),
                SortState.CategoryAsc => operations.OrderBy(s => s.Category).ToList(),
                SortState.CategoryDesc => operations.OrderByDescending(s => s.Category).ToList(),
                SortState.SumAsc => operations.OrderBy(s => s.SumByn).ToList(),
                SortState.SumDesc => operations.OrderByDescending(s => s.SumByn).ToList(),
                _ => operations.OrderBy(s => s.Date).ToList(),
            };

            BalanceViewModel balanceViewModel = new BalanceViewModel
            {
                CreateIncome = createIncomeViewModel,
                CreateExpense = createExpenseViewModel,
                Operations = operations,
                SearchString = searchString,
                SortOperations = new SortOperationsViewModel(sortOrder)
        };

            return View(balanceViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome(CreateIncomeViewModel createIncome)
        {
            string userId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {

                Income income = _mapper.Map<Income>(createIncome);
                income.UserId = userId;
                await _balanceService.AddIncome(income);
             
                return RedirectToAction("Index");
            }

            List<IncomeCategory> incomeCategory = await _balanceService.GetUserIncomeCategories(userId);
            List<ExpenseCategory> expenseCategory = await _balanceService.GetUserExpenseCategories(userId);
            IEnumerable<Currency> currency = await _balanceService.GetCurrency();
            SelectList currencySelectList = new SelectList(currency, "Id", "Code");

            CreateExpenseViewModel createExpenseViewModel = new CreateExpenseViewModel
            {
                Categories = new SelectList(expenseCategory, "Id", "Title"),
                Currencies = currencySelectList
            };

            createIncome.Categories = new SelectList(incomeCategory, "Id", "Title");
            createIncome.Currencies = currencySelectList;

            IReadOnlyList<Income> incomes = await _balanceService.GetUserIncomes(userId, "");
            IReadOnlyList<Expense> expense = await _balanceService.GetUserExpenses(userId, "");

            var operations = _mapper.Map<IReadOnlyList<Income>, List<OperationViewModel>>(incomes);
            operations.AddRange(_mapper.Map<IReadOnlyList<Expense>, List<OperationViewModel>>(expense));

            BalanceViewModel balanceViewModel = new BalanceViewModel
            {
                CreateIncome = createIncome,
                CreateExpense = createExpenseViewModel,
                Operations = operations.OrderBy(x => x.Date).ToList(),
                SortOperations = new SortOperationsViewModel(SortState.DateTimeAsc)
            };

            return View("Index", balanceViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(CreateExpenseViewModel createExpense)
        {
            string userId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                
                if (createExpense.SumByn < await _balanceService.GetBalance(userId, createExpense.Date))
                { 
                    Expense expense = _mapper.Map<Expense>(createExpense);
                    expense.UserId = _userManager.GetUserId(User);
                    await _balanceService.AddExpense(expense);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CreateExpense.Sum", "Не достаточно средств");
                }
            }

            List<IncomeCategory> incomeCategory = await _balanceService.GetUserIncomeCategories(userId);
            List<ExpenseCategory> expenseCategory = await _balanceService.GetUserExpenseCategories(userId);
            IEnumerable<Currency> currency = await _balanceService.GetCurrency();
            SelectList currencySelectList = new SelectList(currency, "Id", "Code");

            CreateIncomeViewModel createIncomeViewModel = new CreateIncomeViewModel
            {
                Categories = new SelectList(incomeCategory, "Id", "Title"),
                Currencies = currencySelectList
            };

            createExpense.Categories = new SelectList(expenseCategory, "Id", "Title");
            createExpense.Currencies = currencySelectList;

            IReadOnlyList<Income> incomes = await _balanceService.GetUserIncomes(userId, "");
            IReadOnlyList<Expense> expenses = await _balanceService.GetUserExpenses(userId, "");

            var operations = _mapper.Map<IReadOnlyList<Income>, List<OperationViewModel>>(incomes);
            operations.AddRange(_mapper.Map<IReadOnlyList<Expense>, List<OperationViewModel>>(expenses));

            BalanceViewModel balanceViewModel = new BalanceViewModel
            {
                CreateIncome = createIncomeViewModel,
                CreateExpense = createExpense,
                Operations = operations.OrderBy(x => x.Date).ToList(),
                SortOperations = new SortOperationsViewModel(SortState.DateTimeAsc)
            };


            return View("Index", balanceViewModel);
        }

        public async Task<IActionResult> WriteExcelFile(string searchString)
        {
            string userId = _userManager.GetUserId(User);

            IReadOnlyList<Income> incomes = await _balanceService.GetUserIncomes(userId, searchString);
            IReadOnlyList<Expense> expense = await _balanceService.GetUserExpenses(userId, searchString);

            var operations = _mapper.Map<IReadOnlyList<Income>, List<OperationExcelViewModel>>(incomes);
            operations.AddRange(_mapper.Map<IReadOnlyList<Expense>, List<OperationExcelViewModel>>(expense));

            DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(operations.OrderBy(x => x.Date).ToList()), (typeof(DataTable)));

            MemoryStream ms = _spreadsheetDocument.DataTableToMemoryStream(table);

            string fileName = "Операции.xlsx";
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            ms.Position = 0;
            return File(ms, fileType, fileName);
        }

        public async Task<JsonResult> Rate(DateTime date, string currency)
        {

            var rate = await _rateService.GetRateOnDate(date, currency);

            return Json(rate);

        }
    }
}
