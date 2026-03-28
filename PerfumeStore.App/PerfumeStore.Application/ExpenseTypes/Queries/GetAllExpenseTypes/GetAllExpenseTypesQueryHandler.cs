using AutoMapper;
using MediatR;
using PerfumeStore.Application.ExpenseTypes.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.ExpenseTypes.Queries.GetAllExpenseTypes {
    public class GetAllExpenseTypesQueryHandler(IExpenseTypesRepository expenseTypesRepository, IMapper mapper) : IRequestHandler<GetAllExpenseTypesQuery, IEnumerable<ExpenseTypeDto>> {
        public async Task<IEnumerable<ExpenseTypeDto>> Handle(GetAllExpenseTypesQuery request, CancellationToken cancellationToken) {
            var expenseTypes = await expenseTypesRepository.GetAllAsync(request.Search);
            return mapper.Map<IEnumerable<ExpenseTypeDto>>(expenseTypes);
        }
    }
}
