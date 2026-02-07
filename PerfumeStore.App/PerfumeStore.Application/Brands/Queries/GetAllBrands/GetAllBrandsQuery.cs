using MediatR;
using PerfumeStore.Application.Brands.Dtos;

namespace PerfumeStore.Application.Brands.Queries.GetAllBrands {
    public class GetAllBrandsQuery(string? search) : IRequest<IEnumerable<BrandDto>> {
        public string? Search { get; set; } = search;
    }
}
