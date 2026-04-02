using MediatR;
using PerfumeStore.Application.ReceiptVouchers.Dtos;

namespace PerfumeStore.Application.ReceiptVouchers.Queries.GetOpenPersonDebts;

public record GetOpenPersonDebtsQuery(Guid PersonId) : IRequest<IEnumerable<OpenPersonDebtDto>>;
