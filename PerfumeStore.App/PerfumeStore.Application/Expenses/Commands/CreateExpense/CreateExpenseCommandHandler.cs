using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Expenses.Commands.CreateExpense {
    public class CreateExpenseCommandHandler(
        IExpensesRepository expensesRepository,
        IMoneyAccountsRepository moneyAccountsRepository,
        IExpenseTypesRepository expenseTypesRepository,
        IMapper mapper) : IRequestHandler<CreateExpenseCommand, long> {

        public async Task<long> Handle(CreateExpenseCommand request, CancellationToken cancellationToken) {
            var moneyAccount = await moneyAccountsRepository.GetByIdAsync(request.MoneyAccountID)
                ?? throw new NotFoundException(nameof(MoneyAccount), request.MoneyAccountID.ToString());

            _ = await expenseTypesRepository.GetByIdAsync(request.ExpenseTypeId)
                ?? throw new NotFoundException(nameof(ExpenseType), request.ExpenseTypeId.ToString());

            moneyAccount.CurrentBalance -= request.Amount;

            var expense = mapper.Map<Expense>(request);
            return await expensesRepository.AddAsync(expense);
        }
    }
}
