using MediatR;
using PerfumeStore.Application.Inventory.Dtos;

namespace PerfumeStore.Application.Inventory.Queries.GetInventoryById {
    public class GetInventoryByIdQuery(int id) : IRequest<InventoryDto> {
        public int Id { get; } = id;
    }
}
