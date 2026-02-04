using MediatR;
using PerfumeStore.Application.Brands.Dtos;

namespace PerfumeStore.Application.Brands.Queries.GetBrandById {
    public class GetBrandByIdQuery(int id) : IRequest<BrandDto> {
        public int ID { get; set; } = id;
    }
}
