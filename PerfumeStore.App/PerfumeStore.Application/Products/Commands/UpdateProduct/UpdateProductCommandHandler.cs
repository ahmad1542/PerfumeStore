using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Products.Commands.UpdateProduct {
    public class UpdateProductCommandHandler(IProductsRepository productsRepository, IMapper mapper) : IRequestHandler<UpdateProductCommand> {
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken) {
            var product = await productsRepository.GetByIdAsync(request.ID);
            if (product == null)
                throw new NotFoundException(nameof(Product), request.ID.ToString());
            
            mapper.Map(request, product);
            await productsRepository.SaveChangesAsync();
        }
    }
}
