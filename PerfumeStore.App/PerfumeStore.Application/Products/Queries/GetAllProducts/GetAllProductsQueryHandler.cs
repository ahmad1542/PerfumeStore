using AutoMapper;
using MediatR;
using PerfumeStore.Application.Products.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Products.Queries.GetAllProducts {
    public class GetAllProductsQueryHandler(IProductsRepository productsRepository, IMapper mapper) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>> {
        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken) {
            var products = await productsRepository.GetAllAsync(request.Search);
            var productsDtos = mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDtos;
        }
    }
}
