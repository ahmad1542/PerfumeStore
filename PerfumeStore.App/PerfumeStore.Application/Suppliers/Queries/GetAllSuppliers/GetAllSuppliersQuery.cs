using MediatR;
using PerfumeStore.Application.Suppliers.Dtos;

namespace PerfumeStore.Application.Suppliers.Queries.GetAllSuppliers {
    public class GetAllSuppliersQuery(string? search) : IRequest<IEnumerable<SupplierDto>> {
        public string? Search { get; } = search;
    }
}
