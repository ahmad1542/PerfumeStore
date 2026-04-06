using AutoMapper;
using MediatR;
using PerfumeStore.Application.ReceiptVouchers.Dtos;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.ReceiptVouchers.Queries.GetReceiptVoucherById;

public class GetReceiptVoucherByIdQueryHandler(IReceiptVouchersRepository receiptVouchersRepository, IMapper mapper) : IRequestHandler<GetReceiptVoucherByIdQuery, ReceiptVoucherDetailsDto?> {
    public async Task<ReceiptVoucherDetailsDto?> Handle(GetReceiptVoucherByIdQuery request, CancellationToken cancellationToken) {
        var item = await receiptVouchersRepository.GetByIdAsync(request.Id);
        if (item is null)
            throw new NotFoundException(nameof(ReceiptVoucher), request.Id.ToString());

        return mapper.Map<ReceiptVoucherDetailsDto>(item);
    }
}
