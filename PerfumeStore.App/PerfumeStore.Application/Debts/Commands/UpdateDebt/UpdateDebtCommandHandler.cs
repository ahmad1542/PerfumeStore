using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;
using System.Security.Principal;

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

            if (account == null)
                throw new Exception("Money account not found");

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