using MediatR;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.Debts.Commands.CreateDebt {
    public class CreateDebtCommand : IRequest<int> {
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }

        public int MoneyAccountId { get; set; }
        public DebtDirection Direction { get; set; } // 1 receivable, 2 payable

        public Guid? PersonId { get; set; }
    }
}
