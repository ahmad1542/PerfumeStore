using MediatR;
using PerfumeStore.Application.Debts.Dtos;

namespace PerfumeStore.Application.Debts.Queries.GetDebtById {
    public class GetDebtByIdQuery(int id) : IRequest<DebtDto> {
        public int Id { get; set; } = id;
    }
}