using MediatR;

namespace PerfumeStore.Application.ExpenseTypes.Commands.CreateExpenseType {
    public class CreateExpenseTypeCommand : IRequest<int> {
        public string Name { get; set; } = null!;
    }
}
