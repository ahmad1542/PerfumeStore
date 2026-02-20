using AutoMapper;
using MediatR;
using PerfumeStore.Application.Suppliers.Dtos;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Suppliers.Queries.GetSupplierById {
    public class GetSupplierByIdQueryHandler(ISuppliersRepository suppliersRepository, IMapper mapper) : IRequestHandler<GetSupplierByIdQuery, SupplierDto> {
        public async Task<SupplierDto> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken) {
            var supplier = await suppliersRepository.GetByIdAsync(request.Id);
            if (supplier is null) 
                throw new NotFoundException(nameof(Supplier), request.Id.ToString());
            
            var supplierDto = mapper.Map<SupplierDto>(supplier);
            return supplierDto;
        }
    }
}
