using MediatR;
using PerfumeStore.Application.PaymentVouchers.Dtos;

namespace PerfumeStore.Application.PaymentVouchers.Queries.GetAllPaymentVouchers;

public class GetAllPaymentVouchersQuery : IRequest<IEnumerable<PaymentVoucherDto>> {
    public string? Search { get; set; }
}
