using MediatR;

namespace PerfumeStore.Application.ReceiptVouchers.Commands.CreateReceiptVoucher;

public class CreateReceiptVoucherCommand : IRequest<long> {
    public DateTime Date { get; set; } = DateTime.Now;
    public string ReceiptForType { get; set; } = "customer";
    public Guid? CustomerId { get; set; }
    public Guid? PersonId { get; set; }
    public int MoneyAccountID { get; set; }
    public decimal Amount { get; set; }
    public string? Notes { get; set; }
    public List<CreateReceiptVoucherSalesApplicationDto> SalesApplications { get; set; } = [];
}

public class CreateReceiptVoucherSalesApplicationDto {
    public long SalesInvoiceId { get; set; }
    public decimal AppliedAmount { get; set; }
}