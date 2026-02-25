using MediatR;
using PerfumeStore.Application.MoneyAccounts.Dtos;

namespace PerfumeStore.Application.MoneyAccounts.Queries.GetMoneyAccountById {
    public class GetMoneyAccountByIdQuery(int id) : IRequest<MoneyAccountDto> {
        public int ID { get; set; } = id;
    }
}
