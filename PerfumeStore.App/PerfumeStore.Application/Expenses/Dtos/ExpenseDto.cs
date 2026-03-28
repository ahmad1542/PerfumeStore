namespace PerfumeStore.Application.Expenses.Dtos {
    public class ExpenseDto {
        public long Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
        public int ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; } = null!;
        public int MoneyAccountID { get; set; }
        public string MoneyAccountName { get; set; } = null!;
    }
}
