namespace PerfumeStore.Application.PaymentVouchers.Dtos;

public class PaymentVoucherDetailsDto {
    public long ID { get; set; }
    public DateTime Date { get; set; }
    public string? SupplierName { get; set; }
    public string? MoneyAccountName { get; set; }
    public decimal Amount { get; set; }
    public string? Notes { get; set; }
    public List<PaymentVoucherApplicationDetailsDto> Applications { get; set; } = [];
}

public class PaymentVoucherApplicationDetailsDto {
    public long PurchaseInvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal AppliedAmount { get; set; }
    public string? InvoiceNotes { get; set; }
}
