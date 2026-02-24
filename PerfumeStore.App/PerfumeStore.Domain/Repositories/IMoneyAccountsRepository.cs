using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Domain.Repositories {
    public interface IMoneyAccountsRepository {
        Task<MoneyAccount?> GetByIdAsync(int id);
        Task<IEnumerable<MoneyAccount>> GetAllAsync(string? search = null);
        Task<int> AddAsync(MoneyAccount moneyAccount);
        Task SaveChangesAsync();
    }
}
