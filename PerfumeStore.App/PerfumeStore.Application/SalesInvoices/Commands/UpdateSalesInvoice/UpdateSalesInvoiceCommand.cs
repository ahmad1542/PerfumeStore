using MediatR;

namespace PerfumeStore.Application.SalesInvoices.Commands.UpdateSalesInvoice {
    public class UpdateSalesInvoiceCommand : IRequest {
        public long ID { get; set; }
    }
}
