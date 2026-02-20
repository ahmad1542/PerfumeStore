using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Customers.Commands.UpdateCustomer {
    public class UpdateCustomerCommandHandler(ICustomersRepository customersRepository, IMapper mapper) : IRequestHandler<UpdateCustomerCommand> {
        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken) {
            var customer = await customersRepository.GetByIdAsync(request.Id);
            if (customer is null) throw new NotFoundException(nameof(Customer), request.Id.ToString());
            
            mapper.Map(request, customer);
            await customersRepository.SaveChangesAsync();
        }
    }
}
