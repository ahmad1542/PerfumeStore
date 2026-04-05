using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Debts.Commands.CreateDebt {
    public class CreateDebtCommandHandler(IDebtsRepository debtsRepository, IMoneyAccountsRepository moneyAccountsRepository, IMapper mapper) : IRequestHandler<CreateDebtCommand, int> {
        public async Task<int> Handle(CreateDebtCommand request, CancellationToken cancellationToken) {

            if (request.PersonId.HasValue) {
                var exists = await debtsRepository.CheckIfPersonExist(request.PersonId.Value);
                if (!exists) {
                    throw new NotFoundException(nameof(Person), request.PersonId.Value.ToString());
                }
            }

            var account = await moneyAccountsRepository.GetByIdAsync(request.MoneyAccountId);
            
            if (account == null)
                throw new NotFoundException(nameof(MoneyAccount), request.MoneyAccountId.ToString());

            var debt = mapper.Map<Debt>(request);
            var id = await debtsRepository.AddAsync(debt);

            if (request.Direction == 1) // Receivable (I gave money)
                account.CurrentBalance -= request.Amount;
            else if (request.Direction == 2) // Payable (I received money)
                account.CurrentBalance += request.Amount;
            await moneyAccountsRepository.SaveChangesAsync();
            return id;
        }
    }
}