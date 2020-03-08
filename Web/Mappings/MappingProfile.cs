using AutoMapper;
using Core.Entities;
using System;
using System.Linq;
using Web.ViewModels;

namespace Web.Mappings
{
    public class MappingProfile: Profile
    {

        public MappingProfile()
        {
            CreateMap<CreateExpenseCategoryViewModel, ExpenseCategory>().ReverseMap();
            CreateMap<CreateIncomeCategoryViewModel, IncomeCategory>().ReverseMap();
            CreateMap<ItemCategoryViewModel, ExpenseCategory>().ReverseMap();
            CreateMap<ItemCategoryViewModel, IncomeCategory>().ReverseMap();
            CreateMap<CreateExpenseViewModel, Expense>().ReverseMap();
            CreateMap<CreateIncomeViewModel, Income>().ReverseMap();
            CreateMap<Income, OperationViewModel>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Title))
                .ForMember(d => d.Currency, o => o.MapFrom(s => s.Currency.Code))
                .ForMember(d => d.Sum, o => o.MapFrom(s => s.Sum.ToString() + s.Currency.Code))
                .ForMember(d => d.SumByn, o => o.MapFrom(s => s.SumByn.ToString()));
            CreateMap<Expense, OperationViewModel>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Title))
                .ForMember(d => d.Currency, o => o.MapFrom(s => s.Currency.Code))
                .ForMember(d => d.Sum, o => o.MapFrom(s => s.Sum.ToString() + s.Currency.Code))
                .ForMember(d => d.SumByn, o => o.MapFrom(s => s.SumByn.ToString()));
            CreateMap<Income, OperationExcelViewModel>()
                .ForMember(d => d.Type, o => o.MapFrom(s => "+"))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Title))
                .ForMember(d => d.Sum, o => o.MapFrom(s => $"{s.Sum.ToString()}  {s.Currency.Code}"))
                .ForMember(d => d.SumByn, o => o.MapFrom(s => s.SumByn.ToString()));
            CreateMap<Expense, OperationExcelViewModel>()
                .ForMember(d => d.Type, o => o.MapFrom(s => "-"))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Title))
                .ForMember(d => d.Sum, o => o.MapFrom(s => $"{s.Sum.ToString()}  {s.Currency.Code}"))
                .ForMember(d => d.SumByn, o => o.MapFrom(s => s.SumByn.ToString()));
        }
    }
}
