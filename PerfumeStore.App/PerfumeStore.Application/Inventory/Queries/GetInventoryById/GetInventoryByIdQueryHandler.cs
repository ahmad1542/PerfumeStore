using AutoMapper;
using MediatR;
using PerfumeStore.Application.Inventory.Dtos;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Inventory.Queries.GetInventoryById {
    public class GetInventoryByIdQueryHandler(IInventoryRepository inventoryRepository, IMapper mapper) : IRequestHandler<GetInventoryByIdQuery, InventoryDto> {
        public async Task<InventoryDto> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken) {
            var inventory = await inventoryRepository.GetByIdAsync(request.Id);
            if (inventory == null) 
                throw new NotFoundException(nameof(Domain.Entities.Inventory), request.Id.ToString());
            return mapper.Map<InventoryDto>(inventory);
        }
    }
}
