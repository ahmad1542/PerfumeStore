using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Domain.Repositories {
    public interface ICustomersRepository {
        Task<Customer?> GetByIdAsync(Guid id);
        Task<IEnumerable<Customer>> GetAllAsync(string? search = null);
        Task<Guid> AddAsync(Customer customer);
        Task SaveChangesAsync();
    }
}
