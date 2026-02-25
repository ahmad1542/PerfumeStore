using AutoMapper;
using MediatR;
using PerfumeStore.Application.MoneyAccounts.Dtos;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.MoneyAccounts.Queries.GetMoneyAccountById {
    public class GetMoneyAccountByIdQueryHandler(IMoneyAccountsRepository moneyAccountsRepository, IMapper mapper) : IRequestHandler<GetMoneyAccountByIdQuery, MoneyAccountDto> {
        public async Task<MoneyAccountDto> Handle(GetMoneyAccountByIdQuery request, CancellationToken cancellationToken) {
            var moneyAccount = await moneyAccountsRepository.GetByIdAsync(request.ID);
            if (moneyAccount == null) 
                throw new NotFoundException(nameof(MoneyAccount), request.ID.ToString());
            return mapper.Map<MoneyAccountDto>(moneyAccount);
        }
    }
}
