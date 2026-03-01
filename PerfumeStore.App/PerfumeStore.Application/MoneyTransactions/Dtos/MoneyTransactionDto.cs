namespace PerfumeStore.Application.MoneyTransactions.Dtos {
    public class MoneyTransactionDto {
        public long ID { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public required string FromMoneyAccountName { get; set; }

        public required string ToMoneyAccountName { get; set; }

        public decimal TransferAmount { get; set; }

        public string? Notes { get; set; }
    }
}
