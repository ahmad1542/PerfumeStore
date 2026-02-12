using MediatR;
using PerfumeStore.Application.ProductCategories.Dtos;

namespace PerfumeStore.Application.ProductCategories.Queries.GetAllProductCategories {
    public class GetAllProductCategoriesQuery(string? search) : IRequest<IEnumerable<ProductCategoryDto>> {
        public string? Search { get; set; } = search;
    }
}
