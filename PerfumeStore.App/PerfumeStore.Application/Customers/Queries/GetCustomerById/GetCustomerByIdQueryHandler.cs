using AutoMapper;
using MediatR;
using PerfumeStore.Application.Customers.Dtos;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Customers.Queries.GetCustomerById {
    public class GetCustomerByIdQueryHandler(ICustomersRepository customersRepository, IMapper mapper) : IRequestHandler<GetCustomerByIdQuery, CustomerDto> {
        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken) {
            var customer = await customersRepository.GetByIdAsync(request.Id);
            if (customer is null)
                throw new NotFoundException(nameof(Customer), request.Id.ToString());

            var customerDto = mapper.Map<CustomerDto>(customer);
            return customerDto;
        }
    }
}
