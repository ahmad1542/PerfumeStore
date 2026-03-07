using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Debts.Commands.CreateDebt {
    public class CreateDebtCommandHandler(IDebtsRepository debtsRepository, IMapper mapper) : IRequestHandler<CreateDebtCommand, int> {
        public async Task<int> Handle(CreateDebtCommand request, CancellationToken cancellationToken) {
            var debt = mapper.Map<Debt>(request);
            var id = await debtsRepository.AddAsync(debt);
            return id;
        }
    }
}
