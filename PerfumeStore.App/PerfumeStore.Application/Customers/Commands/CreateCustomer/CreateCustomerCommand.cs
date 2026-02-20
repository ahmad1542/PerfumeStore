using MediatR;

namespace PerfumeStore.Application.Customers.Commands.CreateCustomer {
    public class CreateCustomerCommand : IRequest<Guid> {
        public string Phone { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
