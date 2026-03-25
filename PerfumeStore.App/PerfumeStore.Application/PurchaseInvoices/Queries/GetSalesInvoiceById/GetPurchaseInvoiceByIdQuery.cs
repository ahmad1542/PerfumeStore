using MediatR;
using PerfumeStore.Application.PurchaseInvoices.Dtos;

namespace PerfumeStore.Application.PurchaseInvoices.Queries.GetSalesInvoiceById {
    public class GetPurchaseInvoiceByIdQuery(long id) : IRequest<PurchaseInvoiceDetailsDto> {
        public long ID { get; set; } = id;
    }
}
