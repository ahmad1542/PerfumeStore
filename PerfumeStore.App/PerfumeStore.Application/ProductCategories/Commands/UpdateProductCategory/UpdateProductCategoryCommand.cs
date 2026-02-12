using MediatR;

namespace PerfumeStore.Application.ProductCategories.Commands.UpdateProductCategory {
    public class UpdateProductCategoryCommand(int id) : IRequest {
        public int ID { get; set; } = id;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

    }
}
