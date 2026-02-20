using MediatR;

namespace PerfumeStore.Application.Customers.Commands.UpdateCustomer {
    public class UpdateCustomerCommand(Guid id) : IRequest {
        public Guid Id { get; set; } = id;
        public string Phone { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
