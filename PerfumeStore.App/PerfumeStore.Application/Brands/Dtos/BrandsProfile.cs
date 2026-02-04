using AutoMapper;
using PerfumeStore.Application.Brands.Commands.CreateBrand;
using PerfumeStore.Application.Brands.Commands.UpdateBrand;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.Brands.Dtos {
    public class BrandsProfile : Profile {
        public BrandsProfile() {
            CreateMap<Brand, BrandDto>();
            CreateMap<CreateBrandCommand, Brand>();
            CreateMap<UpdateBrandCommand, Brand>();
        }
    }
}
