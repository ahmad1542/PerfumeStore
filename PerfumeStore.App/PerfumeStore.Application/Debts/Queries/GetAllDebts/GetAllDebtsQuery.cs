using MediatR;
using PerfumeStore.Application.Debts.Dtos;

namespace PerfumeStore.Application.Debts.Queries.GetAllDebts {
    public class GetAllDebtsQuery(string? search, bool includeSettled) : IRequest<IEnumerable<DebtDto>> {
        public string? Search { get; set; } = search;
        public bool IncludeSettled { get; set; } = includeSettled;
    }
}