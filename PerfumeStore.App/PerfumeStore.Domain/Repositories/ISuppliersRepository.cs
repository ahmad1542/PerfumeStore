using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Domain.Repositories {
    public interface ISuppliersRepository {
        Task<Supplier?> GetByIdAsync(Guid id);
        Task<IEnumerable<Supplier>> GetAllAsync(string? search = null);
        Task<Guid> AddAsync(Supplier supplier);
        Task SaveChangesAsync();
    }
}
