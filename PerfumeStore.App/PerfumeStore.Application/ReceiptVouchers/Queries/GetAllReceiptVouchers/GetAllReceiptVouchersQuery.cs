using MediatR;
using PerfumeStore.Application.ReceiptVouchers.Dtos;

namespace PerfumeStore.Application.ReceiptVouchers.Queries.GetAllReceiptVouchers;

public class GetAllReceiptVouchersQuery : IRequest<IEnumerable<ReceiptVoucherDto>> {
    public string? Search { get; set; }
}
