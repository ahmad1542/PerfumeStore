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

            var from = await moneyAccountsRepository.GetByIdAsync(request.FromMoneyAccountID);
            var to = await moneyAccountsRepository.GetByIdAsync(request.ToMoneyAccountID);

            if (from is null) throw new NotFoundException(nameof(MoneyAccount), request.FromMoneyAccountID.ToString());
            if (to is null) throw new NotFoundException(nameof(MoneyAccount), request.ToMoneyAccountID.ToString());

            if (from.CurrentBalance < request.TransferAmount) throw new BusinessRuleException("Insufficient balance");

            from.CurrentBalance -= request.TransferAmount;
            to.CurrentBalance += request.TransferAmount;

            mapper.Map(request, moneyTransaction);
            await moneyAccountsRepository.SaveChangesAsync();
            await moneyTransactionsRepository.SaveChangesAsync();
        }
    }
}
