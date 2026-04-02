namespace PerfumeStore.Domain.Entities;

public class PaymentVoucher {
    public long ID { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public decimal Amount { get; set; }

    public string? Notes { get; set; }

    public Guid SupplierId { get; set; }

    public int MoneyAccountID { get; set; }

    public MoneyAccount MoneyAccount { get; set; } = null!;

    public Supplier Supplier { get; set; } = null!;

    public ICollection<PaymentVoucherPurchaseInvoice> AppliedPurchaseInvoices { get; set; } = new List<PaymentVoucherPurchaseInvoice>();
}
