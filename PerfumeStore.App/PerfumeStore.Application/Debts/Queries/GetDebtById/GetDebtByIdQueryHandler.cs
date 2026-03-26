using AutoMapper;
using MediatR;
using PerfumeStore.Application.Debts.Dtos;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Debts.Queries.GetDebtById {
    public class GetDebtByIdQueryHandler(IDebtsRepository debtsRepository, IMapper mapper) : IRequestHandler<GetDebtByIdQuery, DebtDto> {
        public async Task<DebtDto> Handle(GetDebtByIdQuery request, CancellationToken cancellationToken) {
            var debt = await debtsRepository.GetByIdAsync(request.Id);
            if (debt is null) {
                throw new NotFoundException(nameof(Debt), request.Id.ToString());
            }

            return mapper.Map<DebtDto>(debt);
        }
    }
}