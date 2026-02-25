using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.MoneyAccounts.Commands.UpdateMoneyAccount {
    public class UpdateMoneyAccountCommandHandler(IMoneyAccountsRepository moneyAccountsRepository, IMapper mapper) : IRequestHandler<UpdateMoneyAccountCommand> {
        public async Task Handle(UpdateMoneyAccountCommand request, CancellationToken cancellationToken) {
            var moneyAccount = await moneyAccountsRepository.GetByIdAsync(request.ID);
            if (moneyAccount == null)
                throw new NotFoundException(nameof(MoneyAccount), request.ID.ToString());
            mapper.Map(request, moneyAccount);
            await moneyAccountsRepository.SaveChangesAsync();
        }
    }
}
