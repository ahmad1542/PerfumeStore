using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.ProductCategories.Commands.CreateProductCategory {
    public class CreateProductCategoryCommandHandler(IProductCategoriesRepository productCategoriesRepository, IMapper mapper) : IRequestHandler<CreateProductCategoryCommand, int> {
        public async Task<int> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken) {
            var productCategory = mapper.Map<ProductCategory>(request);
            int id = await productCategoriesRepository.AddAsync(productCategory);
            return id;
        }
    }
}
