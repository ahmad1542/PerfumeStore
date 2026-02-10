using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Products.Commands.UpdateProduct {
    public class UpdateProductCommandHandler(IProductsRepository productsRepository, IMapper mapper) : IRequestHandler<UpdateProductCommand> {
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken) {
            mapper.Map<Product>(request);
            await productsRepository.SaveChangesAsync();
        }
    }
}
