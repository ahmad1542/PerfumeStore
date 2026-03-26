using MediatR;
using PerfumeStore.Application.PurchaseInvoices.Dtos;

namespace PerfumeStore.Application.PurchaseInvoices.Queries.GetPurchaseInvoiceById {
    public class GetPurchaseInvoiceByIdQuery(long id) : IRequest<PurchaseInvoiceDetailsDto> {
        public long ID { get; set; } = id;
    }
}
