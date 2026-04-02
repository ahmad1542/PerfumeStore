namespace PerfumeStore.Application.ReceiptVouchers.Dtos;

public class OpenSalesInvoiceDto {
    public long SalesInvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal RemainingAmount { get; set; }
    public string? Notes { get; set; }
}
