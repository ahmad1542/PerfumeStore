using AutoMapper;
using PerfumeStore.Application.ProductCategories.Commands.CreateProductCategory;
using PerfumeStore.Application.ProductCategories.Commands.UpdateProductCategory;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.ProductCategories.Dtos {
    public class ProductCategoriesProfile : Profile {
        public ProductCategoriesProfile() {
            CreateMap<ProductCategoryDto, ProductCategory>();
            CreateMap<CreateProductCategoryCommand, ProductCategory>();
            CreateMap<UpdateProductCategoryCommand, ProductCategory>();

        }
    }
}
