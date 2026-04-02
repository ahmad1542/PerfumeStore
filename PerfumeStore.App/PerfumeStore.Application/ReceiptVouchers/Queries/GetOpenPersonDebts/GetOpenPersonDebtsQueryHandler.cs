using AutoMapper;
using MediatR;
using PerfumeStore.Application.ReceiptVouchers.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.ReceiptVouchers.Queries.GetOpenPersonDebts;

public class GetOpenPersonDebtsQueryHandler(IReceiptVouchersRepository receiptVouchersRepository, IMapper mapper) : IRequestHandler<GetOpenPersonDebtsQuery, IEnumerable<OpenPersonDebtDto>> {
    public async Task<IEnumerable<OpenPersonDebtDto>> Handle(GetOpenPersonDebtsQuery request, CancellationToken cancellationToken) {
        var items = await receiptVouchersRepository.GetOpenPersonDebtsAsync(request.PersonId);
        return mapper.Map<IEnumerable<OpenPersonDebtDto>>(items);
    }
}
