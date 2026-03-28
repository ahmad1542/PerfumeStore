using MediatR;
using PerfumeStore.Application.ExpenseTypes.Dtos;

namespace PerfumeStore.Application.ExpenseTypes.Queries.GetExpenseTypeById {
    public class GetExpenseTypeByIdQuery(int id) : IRequest<ExpenseTypeDto?> {
        public int Id { get; set; } = id;
    }
}
