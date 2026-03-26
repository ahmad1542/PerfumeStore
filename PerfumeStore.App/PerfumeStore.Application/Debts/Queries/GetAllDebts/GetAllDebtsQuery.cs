using MediatR;
using PerfumeStore.Application.Debts.Dtos;

namespace PerfumeStore.Application.Debts.Queries.GetAllDebts {
    public class GetAllDebtsQuery(string? search) : IRequest<IEnumerable<DebtDto>> {
        public string? Search { get; set; } = search;
    }
}