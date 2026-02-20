using MediatR;

namespace PerfumeStore.Application.Suppliers.Commands.CreateSupplier {
    public class CreateSupplierCommand : IRequest<Guid> {
        public string Phone { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
