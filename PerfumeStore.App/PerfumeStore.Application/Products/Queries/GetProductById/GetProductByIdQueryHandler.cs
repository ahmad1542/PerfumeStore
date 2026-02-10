using AutoMapper;
using MediatR;
using PerfumeStore.Application.Products.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Products.Queries.GetProductById {
    public class GetProductByIdQueryHandler(IProductsRepository productsRepository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductDto> {
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) {
            var product = await productsRepository.GetByIdAsync(request.ID);
            var productDto = mapper.Map<ProductDto>(product);
            return productDto;
        }
    }
}
