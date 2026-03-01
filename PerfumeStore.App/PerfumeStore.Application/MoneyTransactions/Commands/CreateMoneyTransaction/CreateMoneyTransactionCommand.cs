using MediatR;

namespace PerfumeStore.Application.MoneyTransactions.Commands.CreateMoneyTransaction {
    public class CreateMoneyTransactionCommand : IRequest<long> {
        public DateTime? Date { get; set; }

        public int FromMoneyAccountID { get; set; }

        public int ToMoneyAccountID { get; set; }

        public decimal TransferAmount { get; set; }

        public string? Notes { get; set; }
    }
}
