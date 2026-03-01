using MediatR;
using PerfumeStore.Application.MoneyTransactions.Dtos;

namespace PerfumeStore.Application.MoneyTransactions.Queries.GetMoneyTransactionById {
    public class GetMoneyTransactionByIdQuery(long id) : IRequest<MoneyTransactionDto> {
        public long ID { get; } = id;
    }
}
