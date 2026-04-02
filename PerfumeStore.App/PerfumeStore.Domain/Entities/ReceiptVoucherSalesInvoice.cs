namespace PerfumeStore.Domain.Entities;

public class ReceiptVoucherSalesInvoice {
    public long ID { get; set; }

    public long ReceiptVoucherId { get; set; }
    public ReceiptVoucher ReceiptVoucher { get; set; } = null!;

    public long SalesInvoiceId { get; set; }
    public SalesInvoice SalesInvoice { get; set; } = null!;

    public decimal AppliedAmount { get; set; }
}
