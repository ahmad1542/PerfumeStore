using MediatR;
using PerfumeStore.Application.MoneyTransactions.Dtos;

namespace PerfumeStore.Application.MoneyTransactions.Queries.GetAllMoneyTransactions {
    public class GetAllMoneyTransactionsQuery(string? search) : IRequest<IEnumerable<MoneyTransactionDto>> {
        public string? Search { get; } = search;
    }
}
