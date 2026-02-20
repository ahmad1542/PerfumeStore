using MediatR;
using PerfumeStore.Application.Customers.Dtos;

namespace PerfumeStore.Application.Customers.Queries.GetAllCustomers {
    public class GetAllCustomersQuery(string? search) : IRequest<IEnumerable<CustomerDto>> {
        public string? Search { get; set; } = search;
    }
}
