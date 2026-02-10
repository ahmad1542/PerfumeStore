using MediatR;

namespace PerfumeStore.Application.Products.Commands.UpdateProduct {
    public class UpdateProductCommand(int id) : IRequest {
        public int ID { get; set; } = id;
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public int ProductCategoryID { get; set; }
        public int? BrandID { get; set; }
        public int? MinStock { get; set; }
    }
}
