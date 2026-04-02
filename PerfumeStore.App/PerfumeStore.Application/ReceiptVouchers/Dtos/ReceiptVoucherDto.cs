namespace PerfumeStore.Application.ReceiptVouchers.Dtos;

public class ReceiptVoucherDto {
    public long ID { get; set; }
    public DateTime Date { get; set; }
    public string? PartyName { get; set; }
    public string ReceiptForType { get; set; } = "customer";
    public string? MoneyAccountName { get; set; }
    public decimal Amount { get; set; }
    public int AppliedInvoicesCount { get; set; }
    public int AppliedDebtsCount { get; set; }
    public string? Notes { get; set; }
}
