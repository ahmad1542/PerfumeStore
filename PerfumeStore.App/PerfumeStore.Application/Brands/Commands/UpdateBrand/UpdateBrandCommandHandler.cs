using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Brands.Commands.UpdateBrand {
    public class UpdateBrandCommandHandler(IBrandsRepository brandsRepository, IMapper mapper) : IRequestHandler<UpdateBrandCommand> {
        public async Task Handle(UpdateBrandCommand request, CancellationToken cancellationToken) {
            var brand = await brandsRepository.GetByIdAsync(request.ID);
            if (brand == null)
                throw new NotFoundException(nameof(Brand), request.ID.ToString());

            mapper.Map(request, brand);
            await brandsRepository.SaveChangesAsync();
        }
    }
}
