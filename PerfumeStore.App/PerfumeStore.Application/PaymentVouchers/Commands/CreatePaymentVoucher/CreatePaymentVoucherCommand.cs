using MediatR;

namespace PerfumeStore.Application.PaymentVouchers.Commands.CreatePaymentVoucher;

public class CreatePaymentVoucherCommand : IRequest<long> {
    public DateTime Date { get; set; } = DateTime.Now;
    public Guid SupplierId { get; set; }
    public int MoneyAccountID { get; set; }
    public decimal Amount { get; set; }
    public string? Notes { get; set; }
    public List<CreatePaymentVoucherApplicationDto> Applications { get; set; } = [];
}

public class CreatePaymentVoucherApplicationDto {
    public long PurchaseInvoiceId { get; set; }
    public decimal AppliedAmount { get; set; }
}
