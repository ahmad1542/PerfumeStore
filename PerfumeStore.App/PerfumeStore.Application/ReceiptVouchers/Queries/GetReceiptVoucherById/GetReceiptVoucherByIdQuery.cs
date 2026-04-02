using MediatR;
using PerfumeStore.Application.ReceiptVouchers.Dtos;

namespace PerfumeStore.Application.ReceiptVouchers.Queries.GetReceiptVoucherById;

public record GetReceiptVoucherByIdQuery(long Id) : IRequest<ReceiptVoucherDetailsDto?>;
