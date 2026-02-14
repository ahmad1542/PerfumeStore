using AutoMapper;
using MediatR;
using PerfumeStore.Application.Products.Dtos;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Products.Queries.GetProductById {
    public class GetProductByIdQueryHandler(IProductsRepository productsRepository, IProductCategoriesRepository productCategoriesRepository, IBrandsRepository brandsRepository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductDto> {
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) {
            var product = await productsRepository.GetByIdAsync(request.ID);
            if (product == null)
                throw new NotFoundException(nameof(Product), request.ID.ToString());
            var productDto = mapper.Map<ProductDto>(product);
            return productDto;
        }
    }
}
