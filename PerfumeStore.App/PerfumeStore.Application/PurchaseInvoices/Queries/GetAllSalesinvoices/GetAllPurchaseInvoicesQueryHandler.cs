using AutoMapper;
using MediatR;
using PerfumeStore.Application.PurchaseInvoices.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.PurchaseInvoices.Queries.GetAllPurchaseinvoices {
    public class GetAllPurchaseInvoicesQueryHandler(IPurchaseInvoicesRepository purchaseInvoicesRepository, IMapper mapper) : IRequestHandler<GetAllPurchaseinvoicesQuery, IEnumerable<PurchaseInvoiceDto>> {

        public async Task<IEnumerable<PurchaseInvoiceDto>> Handle(GetAllPurchaseinvoicesQuery request, CancellationToken cancellationToken) {
            var purchaseInvoices = await purchaseInvoicesRepository.GetAllAsync(
                request.Search,
                request.FromDate,
                request.ToDate
            );

            return mapper.Map<IEnumerable<PurchaseInvoiceDto>>(purchaseInvoices);
        }
    }
}
