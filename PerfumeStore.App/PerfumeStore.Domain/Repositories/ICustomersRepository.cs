using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Domain.Repositories {
    public interface ICustomersRepository {
        Task<Customer?> GetByPhoneNoAsync(string phone);
        Task<IEnumerable<Customer>> GetAllAsync(string? search = null);
        Task<string> AddAsync(Customer customer);
        Task SaveChangesAsync();
    }
}
