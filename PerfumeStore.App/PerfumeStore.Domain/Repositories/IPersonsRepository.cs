using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Domain.Repositories {
    public interface IPersonsRepository {
        Task<Person?> GetByPhoneNoAsync(string phone);
        Task<IEnumerable<Person>> GetAllAsync(string? search = null);
        Task<string> AddAsync(Person person);
        Task SaveChangesAsync();
    }
}
