namespace PerfumeStore.Domain.Entities;

public class PaymentVoucherPurchaseInvoice {
    public long ID { get; set; }

    public long PaymentVoucherId { get; set; }
    public PaymentVoucher PaymentVoucher { get; set; } = null!;

    public long PurchaseInvoiceId { get; set; }
    public PurchaseInvoice PurchaseInvoice { get; set; } = null!;

    public decimal AppliedAmount { get; set; }
}
