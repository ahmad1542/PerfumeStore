using MediatR;
using PerfumeStore.Application.ExpenseTypes.Dtos;

namespace PerfumeStore.Application.ExpenseTypes.Queries.GetAllExpenseTypes {
    public class GetAllExpenseTypesQuery(string? search = null) : IRequest<IEnumerable<ExpenseTypeDto>> {
        public string? Search { get; set; } = search;
    }
}
