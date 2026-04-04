using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.Debts.Dtos {
    public class DebtDto {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
        public int MoneyAccountId { get; set; }
        public int Direction { get; set; } // 1 receivable, 2 payable

        public long? SalesInvoiceId { get; set; }
        public long? PurchaseInvoiceId { get; set; }
        public Guid? PersonId { get; set; }
        public string? PersonPhone { get; set; }
        public string? PersonName { get; set; }
        public string? PartyType { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
