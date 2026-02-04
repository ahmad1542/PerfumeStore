using AutoMapper;
using MediatR;
using PerfumeStore.Application.Brands.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Brands.Queries.GetAllBrands {
    public class GetAllBrandsQueryHandler(IBrandsRepository brandsRepository, IMapper mapper) : IRequestHandler<GetAllBrandsQuery, IEnumerable<BrandDto>> {
        public async Task<IEnumerable<BrandDto>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken) {
            var brands = await brandsRepository.GetAllAsync() ?? throw new Exception("There is no Brand exist!");
            var brandsDtos = mapper.Map<IEnumerable<BrandDto>>(brands);
            return brandsDtos;
        }
    }
}
