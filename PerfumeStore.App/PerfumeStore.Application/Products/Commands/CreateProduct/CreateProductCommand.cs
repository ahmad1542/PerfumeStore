using MediatR;

namespace PerfumeStore.Application.Products.Commands.CreateProduct {
    public class CreateProductCommand : IRequest<int> {
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public int? MinStock { get; set; }

        public int? BrandID { get; set; }
        public int ProductCategoryID { get; set; }
    }
}