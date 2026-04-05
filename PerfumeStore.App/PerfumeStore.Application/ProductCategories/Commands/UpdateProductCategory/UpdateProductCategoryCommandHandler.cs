using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.ProductCategories.Commands.UpdateProductCategory {
    public class UpdateProductCategoryCommandHandler(IProductCategoriesRepository productCategoriesRepository, IMapper mapper) : IRequestHandler<UpdateProductCategoryCommand> {
        public async Task Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken) {
            var productCategory = await productCategoriesRepository.GetByIdAsync(request.ID);
            if (productCategory == null)
                throw new NotFoundException(nameof(ProductCategory), request.ID.ToString());

            mapper.Map(request, productCategory);
            await productCategoriesRepository.SaveChangesAsync();
        }
    }
}
