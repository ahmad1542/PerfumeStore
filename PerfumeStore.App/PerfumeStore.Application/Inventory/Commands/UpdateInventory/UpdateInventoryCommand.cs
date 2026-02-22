using MediatR;
using PerfumeStore.Application.Inventory.Dtos;

namespace PerfumeStore.Application.Inventory.Commands.UpdateInventory {
    public class UpdateInventoryCommand : IRequest<InventoryDto> {
        public int ProductID { get; set; }
    }
}
