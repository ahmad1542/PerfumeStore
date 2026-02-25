using AutoMapper;
using MediatR;
using PerfumeStore.Application.MoneyAccounts.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.MoneyAccounts.Queries.GetAllMoneyAccounts {
    public class GetAllMoneyAccountsQueryHandler(IMoneyAccountsRepository moneyAccountsRepository, IMapper mapper) : IRequestHandler<GetAllMoneyAccountsQuery, IEnumerable<MoneyAccountDto>> {
        public async Task<IEnumerable<MoneyAccountDto>> Handle(GetAllMoneyAccountsQuery request, CancellationToken cancellationToken) {
            var moneyAccounts = await moneyAccountsRepository.GetAllAsync(request.Search);
            return mapper.Map<IEnumerable<MoneyAccountDto>>(moneyAccounts);
        }
    }
}
