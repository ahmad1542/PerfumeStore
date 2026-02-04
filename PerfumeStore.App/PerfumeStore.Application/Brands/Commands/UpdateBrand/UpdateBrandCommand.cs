using MediatR;

namespace PerfumeStore.Application.Brands.Commands.UpdateBrand {
    public class UpdateBrandCommand(int id) : IRequest {
        public int ID { get; set; } = id;
        public string Name { get; set; } = default!;
        public string? BrandDescription { get; set; }
    }
}
