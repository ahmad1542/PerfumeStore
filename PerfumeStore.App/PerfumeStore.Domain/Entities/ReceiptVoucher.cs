namespace PerfumeStore.Domain.Entities;

public class ReceiptVoucher {
    public long ID { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public decimal Amount { get; set; }

    public string? Notes { get; set; }

    public Guid? CustomerId { get; set; }
    public Guid? PersonId { get; set; }

    public int MoneyAccountID { get; set; }

    public Customer? Customer { get; set; }
    public Person? Person { get; set; }

    public MoneyAccount MoneyAccount { get; set; } = null!;

    public ICollection<ReceiptVoucherSalesInvoice> AppliedSalesInvoices { get; set; } = new List<ReceiptVoucherSalesInvoice>();
    public ICollection<ReceiptVoucherDebt> AppliedPersonDebts { get; set; } = new List<ReceiptVoucherDebt>();
}
