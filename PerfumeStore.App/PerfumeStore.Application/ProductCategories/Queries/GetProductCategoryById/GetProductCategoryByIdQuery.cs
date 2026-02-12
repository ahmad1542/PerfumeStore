using MediatR;
using PerfumeStore.Application.Products.Dtos;

namespace PerfumeStore.Application.ProductCategories.Queries.GetProductCategoryById {
    public class GetProductCategoryByIdQuery(int id) : IRequest<ProductDto> {
        public int ID { get; set; } = id;
    }
}
