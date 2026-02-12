using MediatR;

namespace PerfumeStore.Application.ProductCategories.Commands.CreateProductCategory {
    public class CreateProductCategoryCommand : IRequest<int> {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
