using AutoMapper;
using MediatR;
using PerfumeStore.Application.PaymentVouchers.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.PaymentVouchers.Queries.GetPaymentVoucherById;

public class GetPaymentVoucherByIdQueryHandler(IPaymentVouchersRepository paymentVouchersRepository, IMapper mapper) : IRequestHandler<GetPaymentVoucherByIdQuery, PaymentVoucherDetailsDto?> {
    public async Task<PaymentVoucherDetailsDto?> Handle(GetPaymentVoucherByIdQuery request, CancellationToken cancellationToken) {
        var item = await paymentVouchersRepository.GetByIdAsync(request.Id);
        return item == null ? null : mapper.Map<PaymentVoucherDetailsDto>(item);
    }
}
