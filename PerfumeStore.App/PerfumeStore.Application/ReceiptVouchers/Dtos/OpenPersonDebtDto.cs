namespace PerfumeStore.Application.ReceiptVouchers.Dtos;

public class OpenPersonDebtDto {
    public int DebtId { get; set; }
    public DateTime Date { get; set; }
    public decimal RemainingAmount { get; set; }
    public string? Notes { get; set; }
    public string? PersonName { get; set; }
}
