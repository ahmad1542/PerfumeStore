using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Domain.Repositories {
    public interface IDebtsRepository {
        Task<Debt?> GetByIdAsync(int id);
        Task<IEnumerable<Debt>> GetAllAsync(string? search = null);
        Task<int> AddAsync(Debt debt);
        Task SaveChangesAsync();
    }
}
