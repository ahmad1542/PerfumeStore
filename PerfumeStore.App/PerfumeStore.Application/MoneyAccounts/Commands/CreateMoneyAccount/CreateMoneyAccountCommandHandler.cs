using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.MoneyAccounts.Commands.CreateMoneyAccount {
    public class CreateMoneyAccountCommandHandler(IMoneyAccountsRepository moneyAccountsRepository, IMapper mapper) : IRequestHandler<CreateMoneyAccountCommand, int> {
        public async Task<int> Handle(CreateMoneyAccountCommand request, CancellationToken cancellationToken) {
            var moneyAccount = mapper.Map<MoneyAccount>(request);
            int id = await moneyAccountsRepository.AddAsync(moneyAccount);
            return id;
        }
    }
}
