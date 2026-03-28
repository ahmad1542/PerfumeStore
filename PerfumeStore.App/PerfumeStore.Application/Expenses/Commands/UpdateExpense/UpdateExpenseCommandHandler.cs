using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Expenses.Commands.UpdateExpense {
    public class UpdateExpenseCommandHandler(
        IExpensesRepository expensesRepository,
        IMoneyAccountsRepository moneyAccountsRepository,
        IExpenseTypesRepository expenseTypesRepository,
        IMapper mapper) : IRequestHandler<UpdateExpenseCommand> {

        public async Task Handle(UpdateExpenseCommand request, CancellationToken cancellationToken) {
            var expense = await expensesRepository.GetByIdAsync(request.ID);
            if (expense == null)
                throw new NotFoundException(nameof(Expense), request.ID.ToString());

            var oldMoneyAccountId = expense.MoneyAccountID;
            var oldAmount = expense.Amount;

            var oldMoneyAccount = await moneyAccountsRepository.GetByIdAsync(oldMoneyAccountId)
                ?? throw new NotFoundException(nameof(MoneyAccount), oldMoneyAccountId.ToString());

            oldMoneyAccount.CurrentBalance += oldAmount;

            var newMoneyAccount = await moneyAccountsRepository.GetByIdAsync(request.MoneyAccountID)
                ?? throw new NotFoundException(nameof(MoneyAccount), request.MoneyAccountID.ToString());

            _ = await expenseTypesRepository.GetByIdAsync(request.ExpenseTypeId)
                ?? throw new NotFoundException(nameof(ExpenseType), request.ExpenseTypeId.ToString());

            mapper.Map(request, expense);
            newMoneyAccount.CurrentBalance -= request.Amount;

            await expensesRepository.SaveChangesAsync();
        }
    }
}
