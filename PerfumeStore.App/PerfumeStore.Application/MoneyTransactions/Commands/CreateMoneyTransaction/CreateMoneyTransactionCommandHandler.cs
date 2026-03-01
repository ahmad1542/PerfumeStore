using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.MoneyTransactions.Commands.CreateMoneyTransaction {
    public class CreateMoneyTransactionCommandHandler(IMoneyTransactionsRepository moneyTransactionsRepository, IMoneyAccountsRepository moneyAccountsRepository, IMapper mapper) : IRequestHandler<CreateMoneyTransactionCommand, long> {
        public async Task<long> Handle(CreateMoneyTransactionCommand request, CancellationToken cancellationToken) {
            request.Date ??= DateTime.Now;

            var from = await moneyAccountsRepository.GetByIdAsync(request.FromMoneyAccountID);
            var to = await moneyAccountsRepository.GetByIdAsync(request.ToMoneyAccountID);

            if (from is null) throw new NotFoundException(nameof(MoneyAccount), request.FromMoneyAccountID.ToString());
            if (to is null) throw new NotFoundException(nameof(MoneyAccount), request.ToMoneyAccountID.ToString());

            if (from.CurrentBalance < request.TransferAmount) throw new BusinessRuleException("Insufficient balance");

            from.CurrentBalance -= request.TransferAmount;
            to.CurrentBalance += request.TransferAmount;

            var moneyTransaction = mapper.Map<MoneyTransaction>(request);
            await moneyAccountsRepository.SaveChangesAsync();
            long id = await moneyTransactionsRepository.AddAsync(moneyTransaction);
            return id;
        }
    }
}
