using MediatR;
using PerfumeStore.Application.SalesInvoices.Dtos;

namespace PerfumeStore.Application.SalesInvoices.Queries.GetSalesInvoiceById {
    public class GetSalesInvoiceByIdQuery(long id) : IRequest<SalesInvoiceDto> {
        public long ID { get; set; } = id;
    }
}
