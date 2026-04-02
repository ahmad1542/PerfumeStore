using MediatR;
using PerfumeStore.Application.PaymentVouchers.Dtos;

namespace PerfumeStore.Application.PaymentVouchers.Queries.GetOpenPurchaseInvoices;

public record GetOpenPurchaseInvoicesQuery(Guid SupplierId) : IRequest<IEnumerable<OpenPurchaseInvoiceDto>>;
