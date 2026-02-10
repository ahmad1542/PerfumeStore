using MediatR;
using PerfumeStore.Application.Products.Dtos;

namespace PerfumeStore.Application.Products.Queries.GetProductById {
    public class GetProductByIdQuery(int id) : IRequest<ProductDto> {
        public int ID { get; set; } = id;
    }
}
