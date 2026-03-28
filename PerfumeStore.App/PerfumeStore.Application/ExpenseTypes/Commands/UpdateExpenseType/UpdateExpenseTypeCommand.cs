using MediatR;

namespace PerfumeStore.Application.ExpenseTypes.Commands.UpdateExpenseType {
    public class UpdateExpenseTypeCommand(int id) : IRequest {
        public int Id { get; set; } = id;
        public string Name { get; set; } = null!;
    }
}
