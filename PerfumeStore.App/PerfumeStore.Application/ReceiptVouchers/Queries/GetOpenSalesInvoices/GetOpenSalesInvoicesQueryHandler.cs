using AutoMapper;
using MediatR;
using PerfumeStore.Application.ReceiptVouchers.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.ReceiptVouchers.Queries.GetOpenSalesInvoices;

public class GetOpenSalesInvoicesQueryHandler(IReceiptVouchersRepository receiptVouchersRepository, IMapper mapper) : IRequestHandler<GetOpenSalesInvoicesQuery, IEnumerable<OpenSalesInvoiceDto>> {
    public async Task<IEnumerable<OpenSalesInvoiceDto>> Handle(GetOpenSalesInvoicesQuery request, CancellationToken cancellationToken) {
        var items = await receiptVouchersRepository.GetOpenSalesInvoicesAsync(request.CustomerId);
        return mapper.Map<IEnumerable<OpenSalesInvoiceDto>>(items);
    }
}
