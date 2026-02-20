using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Suppliers.Commands.CreateSupplier {
    public class CreateSupplierCommandHandler(ISuppliersRepository suppliersRepository, IMapper mapper) : IRequestHandler<CreateSupplierCommand, Guid> {
        public async Task<Guid> Handle(CreateSupplierCommand request, CancellationToken cancellationToken) {
            var person = mapper.Map<Supplier>(request);
            Guid id = await suppliersRepository.AddAsync(person);
            return id;
        }
    }
}
