using AutoMapper;
using PerfumeStore.Application.Inventory.Commands.CreateInventory;
using PerfumeStore.Application.Inventory.Commands.UpdateInventory;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.Inventory.Dtos {
    public class InventoryProfile : Profile {
        public InventoryProfile() {
            CreateMap<Domain.Entities.Inventory, InventoryDto>()
                .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.Name));
             CreateMap<CreateInventoryCommand, Domain.Entities.Inventory>();
             CreateMap<UpdateInventoryCommand, Domain.Entities.Inventory>();
        }
    }
}
