using MediatR;
using PerfumeStore.Application.Expenses.Dtos;

namespace PerfumeStore.Application.Expenses.Queries.GetExpenseById {
    public class GetExpenseByIdQuery(long id) : IRequest<ExpenseDto> {
        public long ID { get; set; } = id;
    }
}
