using AutoMapper;
using MediatR;
using PerfumeStore.Application.Expenses.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Expenses.Queries.GetExpenseById {
    public class GetExpenseByIdQueryHandler(IExpensesRepository expensesRepository, IMapper mapper) : IRequestHandler<GetExpenseByIdQuery, ExpenseDto> {
        public async Task<ExpenseDto> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken) {
            var expense = await expensesRepository.GetByIdAsync(request.ID);
            var expenseDto = mapper.Map<ExpenseDto>(expense);
            return expenseDto;
        }
    }
}
