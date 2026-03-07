using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.Debts.Dtos {
    public class DebtDto {
        public int Id { get; set; }

        public decimal Amount { get; set; }
        public string? Notes { get; set; }

        public long? SalesInvoiceId { get; set; }

        public long? PurchaseInvoiceId { get; set; }

        public string? PersonPhone { get; set; }
    }
}
