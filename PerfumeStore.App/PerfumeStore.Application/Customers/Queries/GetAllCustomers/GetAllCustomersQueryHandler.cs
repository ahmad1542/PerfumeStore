using AutoMapper;
using MediatR;
using PerfumeStore.Application.Customers.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Customers.Queries.GetAllCustomers {
    public class GetAllCustomersQueryHandler(ICustomersRepository customersRepository, IMapper mapper) : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>> {
        public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken) {
            var cutomers = await customersRepository.GetAllAsync(request.Search);
            return mapper.Map<IEnumerable<CustomerDto>>(cutomers);
        }
    }
}
