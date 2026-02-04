using AutoMapper;
using MediatR;
using PerfumeStore.Application.Brands.Dtos;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Brands.Queries.GetBrandById {
    public class GetBrandByIdQueryHandler(IBrandsRepository brandsRepository, IMapper mapper) : IRequestHandler<GetBrandByIdQuery, BrandDto> {
        public async Task<BrandDto> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken) {
            var brand = await brandsRepository.GetByIdAsync(request.ID);
            if (brand == null)
                throw new NotFoundException(nameof(Brand), request.ID.ToString());
            var brandDto = mapper.Map<BrandDto>(brand);
            return brandDto;
        }
    }
}
