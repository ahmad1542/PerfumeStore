using AutoMapper;
using MediatR;
using PerfumeStore.Application.SalesInvoices.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.SalesInvoices.Queries.GetAllSalesinvoices {
    public class GetAllSalesInvoicesQueryHandler(ISalesInvoicesRepository salesInvoicesRepository, IMapper mapper) : IRequestHandler<GetAllSalesinvoicesQuery, IEnumerable<SalesInvoiceDto>> {

        public async Task<IEnumerable<SalesInvoiceDto>> Handle(GetAllSalesinvoicesQuery request, CancellationToken cancellationToken) {
            var salesInvoices = await salesInvoicesRepository.GetAllAsync(
                request.Search,
                request.FromDate,
                request.ToDate
            );

            return mapper.Map<IEnumerable<SalesInvoiceDto>>(salesInvoices);
        }
    }
}
