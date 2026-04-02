namespace PerfumeStore.Application.PaymentVouchers.Dtos;

public class PaymentVoucherDto {
    public long ID { get; set; }
    public DateTime Date { get; set; }
    public string? SupplierName { get; set; }
    public string? MoneyAccountName { get; set; }
    public decimal Amount { get; set; }
    public int AppliedInvoicesCount { get; set; }
    public string? Notes { get; set; }
}
