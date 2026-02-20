using AutoMapper;
using MediatR;
using PerfumeStore.Application.Suppliers.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Suppliers.Queries.GetAllSuppliers {
    public class GetAllSuppliersQueryHandler(ISuppliersRepository suppliersRepository, IMapper mapper) : IRequestHandler<GetAllSuppliersQuery, IEnumerable<SupplierDto>> {
        public async Task<IEnumerable<SupplierDto>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken) {
            var suppliers = await suppliersRepository.GetAllAsync(request.Search);
            return mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }
    }
}
