using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Debts.Commands.UpdateDebt {
    public class UpdateDebtCommandHandler(IDebtsRepository debtsRepository, IMoneyAccountsRepository moneyAccountsRepository, IMapper mapper) : IRequestHandler<UpdateDebtCommand> {
        public async Task Handle(UpdateDebtCommand request, CancellationToken cancellationToken) {
            var debt = await debtsRepository.GetByIdAsync(request.Id);
            if (debt is null) {
                throw new NotFoundException(nameof(Debt), request.Id.ToString());
            }

            if (request.PersonId.HasValue) {
                var exists = await debtsRepository.CheckIfPersonExist(request.PersonId.Value);
                if (!exists) {
                    throw new NotFoundException(nameof(Person), request.PersonId.Value.ToString());
                }
            }
            var account = await moneyAccountsRepository.GetByIdAsync(request.MoneyAccountId);
            MoneyAccount? oldAccount = null;
            if (debt.Amount > 0 && debt.MoneyAccountId.HasValue)
                oldAccount = await moneyAccountsRepository.GetByIdAsync((int)debt.MoneyAccountId);

            if (account == null)
                throw new NotFoundException(nameof(MoneyAccount), request.MoneyAccountId.ToString());
            if (oldAccount == null)
                throw new NotFoundException(nameof(MoneyAccount), debt.MoneyAccountId.ToString()!);

            if (debt.Direction == 1) // Receivable (I gave money)
                oldAccount.CurrentBalance += debt.Amount;
            else if (debt.Direction == 2) // Payable (I received money)
                oldAccount.CurrentBalance -= debt.Amount;

            mapper.Map(request, debt);
            await debtsRepository.SaveChangesAsync();
            if (request.Direction == 1) // Receivable (I gave money)
                account.CurrentBalance -= request.Amount;
            else if (request.Direction == 2) // Payable (I received money)
                account.CurrentBalance += request.Amount;
            await moneyAccountsRepository.SaveChangesAsync();
        }
    }
}