using AutoMapper;
using MediatR;
using PerfumeStore.Application.Inventory.Dtos;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Inventory.Commands.UpdateInventory {
    public class UpdateInventoryCommandHandler(IInventoryRepository inventoryRepository, IMapper mapper) : IRequestHandler<UpdateInventoryCommand, InventoryDto> {
        public async Task<InventoryDto> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken) {
            var inventory = await inventoryRepository.GetByIdAsync(request.ProductID);
            if (inventory == null) 
                throw new NotFoundException(nameof(Domain.Entities.Inventory), request.ProductID.ToString());
            return mapper.Map<InventoryDto>(inventory);
        }
    }
}
