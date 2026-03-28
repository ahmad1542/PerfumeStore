using AutoMapper;
using PerfumeStore.Application.Expenses.Commands.CreateExpense;
using PerfumeStore.Application.Expenses.Commands.UpdateExpense;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.Expenses.Dtos {
    public class ExpensesProfile : Profile {
        public ExpensesProfile() {
            CreateMap<Expense, ExpenseDto>()
                .ForMember(d => d.ExpenseTypeName, opt => opt.MapFrom(s => s.ExpenseType.Name))
                .ForMember(d => d.MoneyAccountName, opt => opt.MapFrom(s => s.MoneyAccount.AccountName));

            CreateMap<CreateExpenseCommand, Expense>();
            CreateMap<UpdateExpenseCommand, Expense>();
        }
    }
}
