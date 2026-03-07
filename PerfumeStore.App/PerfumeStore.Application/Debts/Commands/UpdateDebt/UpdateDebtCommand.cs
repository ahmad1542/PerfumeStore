using MediatR;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.Debts.Commands.UpdateDebt {
    public class UpdateDebtCommand(int id) : IRequest {
        public int Id { get; set; } = id;

        public decimal Amount { get; set; }
        public string? Notes { get; set; }

        public long? SalesInvoiceId { get; set; }

        public long? PurchaseInvoiceId { get; set; }

        public Guid? PersonId { get; set; }
    }
}
