using MediatR;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.Debts.Commands.UpdateDebt {
    public class UpdateDebtCommand(int id) : IRequest {
        public int Id { get; set; } = id;

        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }

        public int MoneyAccountId { get; set; }
        public int Direction { get; set; } // 1 receivable, 2 payable

        public Guid? PersonId { get; set; }
    }
}
