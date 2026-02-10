using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Domain.Repositories {
    public interface IProductCategoriesRepository {
        Task<ProductCategory?> GetByIdAsync(int id);
        Task<IEnumerable<ProductCategory>> GetAllAsync(string? search = null);
        Task<int> AddAsync(ProductCategory productCategory);
        Task SaveChangesAsync();
    }
}
