using MediatR;
using PerfumeStore.Application.Brands.Dtos;

namespace PerfumeStore.Application.Brands.Queries.GetAllBrands {
    public class GetAllBrandsQuery : IRequest<IEnumerable<BrandDto>> {
    }
}
