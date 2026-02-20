using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Domain.Repositories {
    public interface IPersonsRepository {
        Task<Person?> GetByIdAsync(Guid id);
        Task<IEnumerable<Person>> GetAllAsync(string? search = null);
        Task<Guid> AddAsync(Person person);
        Task SaveChangesAsync();
    }
}
