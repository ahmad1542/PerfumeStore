using MediatR;

namespace PerfumeStore.Application.MoneyAccounts.Commands.UpdateMoneyAccount {
    public class UpdateMoneyAccountCommand(int id) : IRequest {
        public int ID { get; set; } = id;

        public string AccountName { get; set; } = null!;

        public decimal CurrentBalance { get; set; }

        public string? Notes { get; set; }
    }
}
