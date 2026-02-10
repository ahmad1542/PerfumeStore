using MediatR;
using PerfumeStore.Application.Products.Dtos;

namespace PerfumeStore.Application.Products.Queries.GetAllProducts {
    public class GetAllProductsQuery(string? search) : IRequest<IEnumerable<ProductDto>> {
        public string? Search { get; set; } = search;
    }
}
