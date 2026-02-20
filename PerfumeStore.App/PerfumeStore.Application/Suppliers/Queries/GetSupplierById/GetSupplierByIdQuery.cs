using MediatR;
using PerfumeStore.Application.Suppliers.Dtos;

namespace PerfumeStore.Application.Suppliers.Queries.GetSupplierById {
    public class GetSupplierByIdQuery(Guid id) : IRequest<SupplierDto> {
        public Guid Id { get; } = id;
    }
}
