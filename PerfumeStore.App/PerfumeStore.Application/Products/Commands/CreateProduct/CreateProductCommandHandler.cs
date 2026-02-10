using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Products.Commands.CreateProduct {
    public class CreateProductCommandHandler(IProductsRepository productsRepository, IMapper mapper) : IRequestHandler<CreateProductCommand, int> {
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken) {
            var product = mapper.Map<Product>(request);
            return await productsRepository.AddAsync(product);
        }
    }
}