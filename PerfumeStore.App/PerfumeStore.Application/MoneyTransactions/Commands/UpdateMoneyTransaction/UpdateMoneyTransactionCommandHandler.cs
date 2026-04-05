using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.MoneyTransactions.Commands.UpdateMoneyTransaction {
    public class UpdateMoneyTransactionCommandHandler(IMoneyTransactionsRepository moneyTransactionsRepository, IMoneyAccountsRepository moneyAccountsRepository, IMapper mapper) : IRequestHandler<UpdateMoneyTransactionCommand> {
        public async Task Handle(UpdateMoneyTransactionCommand request, CancellationToken cancellationToken) {
            request.Date ??= DateTime.Now;

            var moneyTransaction = await moneyTransactionsRepository.GetByIdAsync(request.ID);
            if (moneyTransaction is null)
                throw new NotFoundException(nameof(MoneyTransaction), request.ID.ToString());

            var oldFrom = await moneyAccountsRepository.GetByIdAsync(moneyTransaction.FromMoneyAccountID);
            var oldTo = await moneyAccountsRepository.GetByIdAsync(moneyTransaction.ToMoneyAccountID);

            if (oldFrom is null)
                throw new NotFoundException(nameof(MoneyAccount), moneyTransaction.FromMoneyAccountID.ToString());

            if (oldTo is null)
                throw new NotFoundException(nameof(MoneyAccount), moneyTransaction.ToMoneyAccountID.ToString());

            var newFrom = await moneyAccountsRepository.GetByIdAsync(request.FromMoneyAccountID);
            var newTo = await moneyAccountsRepository.GetByIdAsync(request.ToMoneyAccountID);

            if (newFrom is null)
                throw new NotFoundException(nameof(MoneyAccount), request.FromMoneyAccountID.ToString());

            if (newTo is null)
                throw new NotFoundException(nameof(MoneyAccount), request.ToMoneyAccountID.ToString());

            // Reverse old transaction effect
            oldFrom.CurrentBalance += moneyTransaction.TransferAmount;
            oldTo.CurrentBalance -= moneyTransaction.TransferAmount;

            // Apply new transaction effect
            newFrom.CurrentBalance -= request.TransferAmount;
            newTo.CurrentBalance += request.TransferAmount;

            mapper.Map(request, moneyTransaction);

            await moneyAccountsRepository.SaveChangesAsync();
            await moneyTransactionsRepository.SaveChangesAsync();
        }
    }
}
