using AutoMapper;
using MediatR;
using PerfumeStore.Application.ReceiptVouchers.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.ReceiptVouchers.Queries.GetReceiptVoucherById;

public class GetReceiptVoucherByIdQueryHandler(IReceiptVouchersRepository receiptVouchersRepository, IMapper mapper) : IRequestHandler<GetReceiptVoucherByIdQuery, ReceiptVoucherDetailsDto?> {
    public async Task<ReceiptVoucherDetailsDto?> Handle(GetReceiptVoucherByIdQuery request, CancellationToken cancellationToken) {
        var item = await receiptVouchersRepository.GetByIdAsync(request.Id);
        return item == null ? null : mapper.Map<ReceiptVoucherDetailsDto>(item);
    }
}
