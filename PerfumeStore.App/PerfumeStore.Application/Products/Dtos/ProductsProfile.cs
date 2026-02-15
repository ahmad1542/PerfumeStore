using AutoMapper;
using PerfumeStore.Application.Products.Commands.CreateProduct;
using PerfumeStore.Application.Products.Commands.UpdateProduct;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.Products.Dtos {
    public class ProductsProfile : Profile {
        public ProductsProfile() {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.BrandName, opt => opt.MapFrom(s => s.Brand != null ? s.Brand.Name : null))
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.ProductCategory != null ? s.ProductCategory.Name : null));
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>()
                .ForMember(d => d.ID, opt => opt.Ignore())
                .ForMember(d => d.Brand, opt => opt.Ignore())
                .ForMember(d => d.Inventory, opt => opt.Ignore())
                .ForMember(d => d.ProductCategory, opt => opt.Ignore()); ;

        }
    }
}
