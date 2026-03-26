using AutoMapper;
using MediatR;
using PerfumeStore.Application.PurchaseInvoices.Dtos;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.PurchaseInvoices.Queries.GetPurchaseInvoiceById {
    public class GetPurchaseInvoiceByIdQueryHandler(IPurchaseInvoicesRepository purchaseInvoicesRepository, IMapper mapper) : IRequestHandler<GetPurchaseInvoiceByIdQuery, PurchaseInvoiceDetailsDto> {
        public async Task<PurchaseInvoiceDetailsDto> Handle(GetPurchaseInvoiceByIdQuery request, CancellationToken cancellationToken) {
            var purchaseInvoice = await purchaseInvoicesRepository.GetByIdAsync(request.ID);
            if (purchaseInvoice == null) 
                throw new NotFoundException(nameof(PurchaseInvoice), request.ID.ToString());
            var purchaseInvoiceDto = mapper.Map<PurchaseInvoiceDetailsDto>(purchaseInvoice);
            return purchaseInvoiceDto;
        }
    }
}
