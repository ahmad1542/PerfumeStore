using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Domain.Repositories {
    public interface IInventoryRepository {
        Task<Inventory?> GetByIdAsync(int id);
        Task<IEnumerable<Inventory>> GetAllAsync(string? search = null);
        Task<int> AddAsync(Inventory inventory);
        Task SaveChangesAsync();
    }
}
