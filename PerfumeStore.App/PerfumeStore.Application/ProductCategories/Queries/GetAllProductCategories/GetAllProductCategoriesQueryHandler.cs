using AutoMapper;
using MediatR;
using PerfumeStore.Application.ProductCategories.Dtos;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.ProductCategories.Queries.GetAllProductCategories {
    public class GetAllProductCategoriesQueryHandler(IProductCategoriesRepository productCategoriesRepository, IMapper mapper) : IRequestHandler<GetAllProductCategoriesQuery, IEnumerable<ProductCategoryDto>> {
        public async Task<IEnumerable<ProductCategoryDto>> Handle(GetAllProductCategoriesQuery request, CancellationToken cancellationToken) {
            var productCategories = await productCategoriesRepository.GetAllAsync(request.Search);
            var productCategoryDtos = mapper.Map<IEnumerable<ProductCategoryDto>>(productCategories);
            return productCategoryDtos;
        }
    }
}
