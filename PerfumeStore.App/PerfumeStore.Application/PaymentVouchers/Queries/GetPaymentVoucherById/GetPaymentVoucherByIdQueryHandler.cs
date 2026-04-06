using AutoMapper;
using MediatR;
using PerfumeStore.Application.PaymentVouchers.Dtos;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.PaymentVouchers.Queries.GetPaymentVoucherById;

public class GetPaymentVoucherByIdQueryHandler(IPaymentVouchersRepository paymentVouchersRepository, IMapper mapper) : IRequestHandler<GetPaymentVoucherByIdQuery, PaymentVoucherDetailsDto?> {
    public async Task<PaymentVoucherDetailsDto?> Handle(GetPaymentVoucherByIdQuery request, CancellationToken cancellationToken) {
        var item = await paymentVouchersRepository.GetByIdAsync(request.Id);
        if (item is null)
            throw new NotFoundException(nameof(PaymentVoucher), request.Id.ToString());
        return mapper.Map<PaymentVoucherDetailsDto>(item);
    }
}
