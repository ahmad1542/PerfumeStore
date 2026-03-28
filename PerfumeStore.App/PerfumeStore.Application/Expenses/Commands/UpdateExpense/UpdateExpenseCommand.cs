using MediatR;

namespace PerfumeStore.Application.Expenses.Commands.UpdateExpense {
    public class UpdateExpenseCommand(long id) : IRequest {
        public long ID { get; set; } = id;
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
        public int ExpenseTypeId { get; set; }
        public int MoneyAccountID { get; set; }
    }
}
