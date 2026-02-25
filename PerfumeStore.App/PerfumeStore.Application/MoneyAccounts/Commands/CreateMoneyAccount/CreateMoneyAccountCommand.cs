using MediatR;

namespace PerfumeStore.Application.MoneyAccounts.Commands.CreateMoneyAccount {
    public class CreateMoneyAccountCommand : IRequest<int> {
        public string AccountName { get; set; } = null!;

        public decimal CurrentBalance { get; set; }

        public string? Notes { get; set; }
    }
}
