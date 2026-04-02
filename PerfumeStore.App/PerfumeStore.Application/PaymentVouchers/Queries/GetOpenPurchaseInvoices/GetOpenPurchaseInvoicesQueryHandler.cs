using AutoMapper;
using MediatR;
using PerfumeStore.Application.PaymentVouchers.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.PaymentVouchers.Queries.GetOpenPurchaseInvoices;

public class GetOpenPurchaseInvoicesQueryHandler(IPaymentVouchersRepository paymentVouchersRepository, IMapper mapper) : IRequestHandler<GetOpenPurchaseInvoicesQuery, IEnumerable<OpenPurchaseInvoiceDto>> {
    public async Task<IEnumerable<OpenPurchaseInvoiceDto>> Handle(GetOpenPurchaseInvoicesQuery request, CancellationToken cancellationToken) {
        var items = await paymentVouchersRepository.GetOpenPurchaseInvoicesAsync(request.SupplierId);
        return mapper.Map<IEnumerable<OpenPurchaseInvoiceDto>>(items);
    }
}
