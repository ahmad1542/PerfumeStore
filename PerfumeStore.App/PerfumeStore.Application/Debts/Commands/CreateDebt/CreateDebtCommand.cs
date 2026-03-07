using MediatR;

namespace PerfumeStore.Application.Debts.Commands.CreateDebt {
    public class CreateDebtCommand : IRequest<int> {
        public decimal Amount { get; set; }
        public string? Notes { get; set; }

        public long? SalesInvoiceId { get; set; }

        public long? PurchaseInvoiceId { get; set; }

        public Guid? PersonId { get; set; }
    }
}
