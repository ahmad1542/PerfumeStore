namespace PerfumeStore.Application.PaymentVouchers.Dtos;

public class OpenPurchaseInvoiceDto {
    public long PurchaseInvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal RemainingAmount { get; set; }
    public string? Notes { get; set; }
}
