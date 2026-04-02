namespace PerfumeStore.Domain.Entities;

public class ReceiptVoucherDebt {
    public long ID { get; set; }

    public long ReceiptVoucherId { get; set; }
    public ReceiptVoucher ReceiptVoucher { get; set; } = null!;

    public int DebtId { get; set; }
    public Debt Debt { get; set; } = null!;

    public decimal AppliedAmount { get; set; }
}
