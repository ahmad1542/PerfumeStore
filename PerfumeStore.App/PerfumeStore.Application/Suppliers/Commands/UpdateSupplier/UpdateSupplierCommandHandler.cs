using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Suppliers.Commands.UpdateSupplier {
    public class UpdateSupplierCommandHandler(ISuppliersRepository suppliersRepository, IMapper mapper) : IRequestHandler<UpdateSupplierCommand> {
        public async Task Handle(UpdateSupplierCommand request, CancellationToken cancellationToken) {
            var person = await suppliersRepository.GetByIdAsync(request.Id);
            if (person is null) 
                throw new NotFoundException(nameof(Supplier), request.Id.ToString());
            mapper.Map(request, person);
            await suppliersRepository.SaveChangesAsync();
        }
    }
}
