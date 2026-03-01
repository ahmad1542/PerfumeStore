using MediatR;

namespace PerfumeStore.Application.MoneyTransactions.Commands.UpdateMoneyTransaction {
    public class UpdateMoneyTransactionCommand(long id) : IRequest {
        public long ID { get; set; } = id;

        public DateTime? Date { get; set; }

        public int FromMoneyAccountID { get; set; }

        public int ToMoneyAccountID { get; set; }

        public decimal TransferAmount { get; set; }

        public string? Notes { get; set; }
    }
}
