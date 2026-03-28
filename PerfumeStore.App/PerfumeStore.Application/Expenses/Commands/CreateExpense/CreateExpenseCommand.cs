using MediatR;

namespace PerfumeStore.Application.Expenses.Commands.CreateExpense {
    public class CreateExpenseCommand : IRequest<long> {
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
        public int ExpenseTypeId { get; set; }
        public int MoneyAccountID { get; set; }
    }
}
