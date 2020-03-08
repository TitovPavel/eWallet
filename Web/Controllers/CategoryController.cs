using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IBalanceService _balanceService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;


        public CategoryController(IBalanceService balanceService,
             IMapper mapper,
            UserManager<User> userManager)
        {
            _balanceService = balanceService;
            _mapper = mapper;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(User);

            var expenseCategories = await _balanceService.GetUserExpenseCategories(userId); 
            var incomeCategories = await _balanceService.GetUserIncomeCategories(userId);

            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                CreateExpense = new CreateExpenseCategoryViewModel(),
                CreateIncome = new CreateIncomeCategoryViewModel(),
                ExpenseCategories = _mapper.Map<IReadOnlyList<ExpenseCategory>, List<ItemCategoryViewModel>>(expenseCategories),
                IncomeCategories = _mapper.Map<IReadOnlyList<IncomeCategory>, List<ItemCategoryViewModel>>(incomeCategories)
            };

            return View(categoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(CreateExpenseCategoryViewModel createExpense)
        {

            string userId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {

                ExpenseCategory expenseCategory = _mapper.Map<ExpenseCategory>(createExpense);
                expenseCategory.UserId = userId;
                await _balanceService.AddExpenseCategory(expenseCategory);

                return RedirectToAction("Index", "Category");
            }

            var expenseCategories = await _balanceService.GetUserExpenseCategories(userId);
            var incomeCategories = await _balanceService.GetUserIncomeCategories(userId);

            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                CreateExpense = createExpense,
                CreateIncome = new CreateIncomeCategoryViewModel(),
                ExpenseCategories = _mapper.Map<IReadOnlyList<ExpenseCategory>, List<ItemCategoryViewModel>>(expenseCategories),
                IncomeCategories = _mapper.Map<IReadOnlyList<IncomeCategory>, List<ItemCategoryViewModel>>(incomeCategories)
            };

            return View("Index", categoryViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> CreateIncome(CreateIncomeCategoryViewModel createIncome)
        {
            string userId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {

                IncomeCategory incomeCategory = _mapper.Map<IncomeCategory>(createIncome);
                incomeCategory.UserId = userId;
                await _balanceService.AddIncomeCategory(incomeCategory);

                return RedirectToAction("Index", "Category");
            }

            var expenseCategories = await _balanceService.GetUserExpenseCategories(userId);
            var incomeCategories = await _balanceService.GetUserIncomeCategories(userId);

            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                CreateExpense = new CreateExpenseCategoryViewModel(),
                CreateIncome = createIncome,
                ExpenseCategories = _mapper.Map<IReadOnlyList<ExpenseCategory>, List<ItemCategoryViewModel>>(expenseCategories),
                IncomeCategories = _mapper.Map<IReadOnlyList<IncomeCategory>, List<ItemCategoryViewModel>>(incomeCategories)

            };

            return View("Index", categoryViewModel);
        }
    }
}
