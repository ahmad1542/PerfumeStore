using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.ExpenseTypes.Commands.UpdateExpenseType {
    public class UpdateExpenseTypeCommandHandler(IExpenseTypesRepository expenseTypesRepository, IMapper mapper) : IRequestHandler<UpdateExpenseTypeCommand> {
        public async Task Handle(UpdateExpenseTypeCommand request, CancellationToken cancellationToken) {
            var expenseType = await expenseTypesRepository.GetByIdAsync(request.Id);
            if (expenseType == null)
                throw new NotFoundException(nameof(ExpenseType), request.Id.ToString());

            mapper.Map(request, expenseType);
            await expenseTypesRepository.SaveChangesAsync();
        }
    }
}
