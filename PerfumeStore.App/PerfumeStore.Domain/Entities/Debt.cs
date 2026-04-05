namespace PerfumeStore.Domain.Entities {
    public class Debt : BaseEntity {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }

        public long? SalesInvoiceId { get; set; }
        public SalesInvoice? SalesInvoice { get; set; }

        public long? PurchaseInvoiceId { get; set; }
        public PurchaseInvoice? PurchaseInvoice { get; set; }

        public Guid? PersonId { get; set; }
        public Person? Person { get; set; }

        public int? MoneyAccountId { get; set; }
        public MoneyAccount? MoneyAccount { get; set; }
          
        public DebtDirection Direction { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public enum DebtDirection {
        Receivable = 1, // I gave money / someone owes me
        Payable = 2     // I received money / I owe someone
    }

}
