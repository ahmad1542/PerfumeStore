using MediatR;
using PerfumeStore.Application.Customers.Dtos;

namespace PerfumeStore.Application.Customers.Queries.GetCustomerById {
    public class GetCustomerByIdQuery(Guid id) : IRequest<CustomerDto> {
        public Guid Id { get; set; } = id;
    }
}
