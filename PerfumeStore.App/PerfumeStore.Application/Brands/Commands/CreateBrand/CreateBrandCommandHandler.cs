using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Brands.Commands.CreateBrand {
    public class CreateBrandCommandHandler(IBrandsRepository brandsRepository, IMapper mapper) : IRequestHandler<CreateBrandCommand, int> {
        public async Task<int> Handle(CreateBrandCommand request, CancellationToken cancellationToken) {
            var brand = mapper.Map<Brand>(request);
            int id = await brandsRepository.AddAsync(brand);
            return id;
        }
    }
}
