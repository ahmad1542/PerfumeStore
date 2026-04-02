namespace PerfumeStore.Application.ReceiptVouchers.Dtos;

public class ReceiptVoucherDetailsDto {
    public long ID { get; set; }
    public DateTime Date { get; set; }
    public string? PartyName { get; set; }
    public string ReceiptForType { get; set; } = "customer";
    public string? MoneyAccountName { get; set; }
    public decimal Amount { get; set; }
    public string? Notes { get; set; }
    public List<ReceiptVoucherApplicationDetailsDto> Applications { get; set; } = [];
}

public class ReceiptVoucherApplicationDetailsDto {
    public string ApplicationType { get; set; } = "salesInvoice";
    public long? SalesInvoiceId { get; set; }
    public int? DebtId { get; set; }
    public DateTime ItemDate { get; set; }
    public decimal AppliedAmount { get; set; }
    public string? Notes { get; set; }
}
