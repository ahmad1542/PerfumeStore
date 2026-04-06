using AutoMapper;
using MediatR;
using PerfumeStore.Application.ExpenseTypes.Dtos;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.ExpenseTypes.Queries.GetExpenseTypeById {
    public class GetExpenseTypeByIdQueryHandler(IExpenseTypesRepository expenseTypesRepository, IMapper mapper) : IRequestHandler<GetExpenseTypeByIdQuery, ExpenseTypeDto?> {
        public async Task<ExpenseTypeDto?> Handle(GetExpenseTypeByIdQuery request, CancellationToken cancellationToken) {
            var expenseType = await expenseTypesRepository.GetByIdAsync(request.Id);

            if (expenseType == null)
                throw new NotFoundException(nameof(ExpenseType), request.Id.ToString());

            return mapper.Map<ExpenseTypeDto?>(expenseType);
        }
    }
}
