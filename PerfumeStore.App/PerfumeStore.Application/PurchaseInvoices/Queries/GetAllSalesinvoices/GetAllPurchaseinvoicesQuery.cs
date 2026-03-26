using MediatR;
using PerfumeStore.Application.PurchaseInvoices.Dtos;

namespace PerfumeStore.Application.PurchaseInvoices.Queries.GetAllPurchaseinvoices {
    public class GetAllPurchaseinvoicesQuery : IRequest<IEnumerable<PurchaseInvoiceDto>> {
        public string? Search { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
