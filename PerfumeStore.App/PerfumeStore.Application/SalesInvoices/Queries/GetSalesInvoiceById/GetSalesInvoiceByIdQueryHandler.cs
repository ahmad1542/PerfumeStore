using AutoMapper;
using MediatR;
using PerfumeStore.Application.SalesInvoices.Dtos;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.SalesInvoices.Queries.GetSalesInvoiceById {
    public class GetSalesInvoiceByIdQueryHandler(ISalesInvoicesRepository salesInvoicesRepository, IMapper mapper) : IRequestHandler<GetSalesInvoiceByIdQuery, SalesInvoiceDto> {
        public async Task<SalesInvoiceDto> Handle(GetSalesInvoiceByIdQuery request, CancellationToken cancellationToken) {
            var salesInvoice = await salesInvoicesRepository.GetByIdAsync(request.ID);
            if (salesInvoice == null) 
                throw new NotFoundException(nameof(SalesInvoice), request.ID.ToString());
            var salesInvoiceDto = mapper.Map<SalesInvoiceDto>(salesInvoice);
            return salesInvoiceDto;
        }
    }
}
