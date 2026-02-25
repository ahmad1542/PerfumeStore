using MediatR;
using PerfumeStore.Application.MoneyAccounts.Dtos;

namespace PerfumeStore.Application.MoneyAccounts.Queries.GetAllMoneyAccounts {
    public class GetAllMoneyAccountsQuery(string? search) : IRequest<IEnumerable<MoneyAccountDto>> {
        public string? Search { get; } = search;
    }
}
