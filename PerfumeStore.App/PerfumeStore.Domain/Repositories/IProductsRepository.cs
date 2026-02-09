using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Domain.Repositories {
    public interface IProductsRepository {
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync(string? search = null);
        Task<int> AddAsync(Product product);
        Task SaveChangesAsync();
    }
}
