using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.ExpenseTypes.Commands.CreateExpenseType {
    public class CreateExpenseTypeCommandHandler(IExpenseTypesRepository expenseTypesRepository, IMapper mapper) : IRequestHandler<CreateExpenseTypeCommand, int> {
        public async Task<int> Handle(CreateExpenseTypeCommand request, CancellationToken cancellationToken) {
            var expenseType = mapper.Map<ExpenseType>(request);
            return await expenseTypesRepository.AddAsync(expenseType);
        }
    }
}
