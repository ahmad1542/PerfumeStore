using MediatR;
using PerfumeStore.Application.ReceiptVouchers.Dtos;

namespace PerfumeStore.Application.ReceiptVouchers.Queries.GetOpenSalesInvoices;

public record GetOpenSalesInvoicesQuery(Guid CustomerId) : IRequest<IEnumerable<OpenSalesInvoiceDto>>;
