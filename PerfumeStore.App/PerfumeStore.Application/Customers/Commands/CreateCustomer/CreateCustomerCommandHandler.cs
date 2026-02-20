using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Customers.Commands.CreateCustomer {
    public class CreateCustomerCommandHandler(ICustomersRepository customersRepository, IMapper mapper) : IRequestHandler<CreateCustomerCommand, Guid> {
        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken) {
            var customer = mapper.Map<Customer>(request);
            Guid id = await customersRepository.AddAsync(customer);
            return id;
        }
    }
}
