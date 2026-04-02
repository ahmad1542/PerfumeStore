using MediatR;
using PerfumeStore.Application.PaymentVouchers.Dtos;

namespace PerfumeStore.Application.PaymentVouchers.Queries.GetPaymentVoucherById;

public record GetPaymentVoucherByIdQuery(long Id) : IRequest<PaymentVoucherDetailsDto?>;
