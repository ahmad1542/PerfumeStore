namespace PerfumeStore.Domain.Entities;

public class SalesInvoice {
    public long ID { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public string? Notes { get; set; }

    public Guid? CustomerId { get; set; }

    public Customer? Customer { get; set; }
    public decimal AmountPaid { get; set; }

    public Debt? Debt { get; set; }

    public int? MoneyAccountId { get; set; }
    public MoneyAccount? MoneyAccount { get; set; }

    public ICollection<ReceiptVoucherSalesInvoice> ReceiptVoucherSalesInvoices { get; private set; } = new List<ReceiptVoucherSalesInvoice>();

    public ICollection<SalesInvoiceItem> SalesInvoiceItems { get; private set; } = new List<SalesInvoiceItem>();
}
