using MediatR;

namespace PerfumeStore.Application.Suppliers.Commands.UpdateSupplier {
    public class UpdateSupplierCommand(Guid id) : IRequest {
        public Guid Id { get; set; } = id;
        public string Phone { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
