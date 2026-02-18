namespace PerfumeStore.Domain.Entities
{
    public class Debt : BaseEntity
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }
        public string? Notes { get; set; }

        public long? SalesInvoiceId { get; set; }
        public SalesInvoice? SalesInvoice { get; set; }

        public long? PurchaseInvoiceId { get; set; }
        public PurchaseInvoice? PurchaseInvoice { get; set; }

        public Guid? PersonId { get; set; }
        public Person? Person { get; set; }
    }

}
