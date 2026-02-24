using Microsoft.EntityFrameworkCore;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;

namespace PerfumeStore.Infrastructure.Repositories {
    public class MoneyAccountsRepository(PerfumeStoreDbContext dbContext) : IMoneyAccountsRepository {
        public async Task<int> AddAsync(MoneyAccount moneyAccount) {
            await dbContext.MoneyAccounts.AddAsync(moneyAccount);
            await dbContext.SaveChangesAsync();
            return moneyAccount.ID;
        }

        public async Task<IEnumerable<MoneyAccount>> GetAllAsync(string? search = null) {
            IQueryable<MoneyAccount> query = dbContext.MoneyAccounts;
            if (!string.IsNullOrWhiteSpace(search)) {
                query = query.Where(c => c.AccountName.Contains(search));
            }
            return await query.ToListAsync();
        }

        public async Task<MoneyAccount?> GetByIdAsync(int id) {
            return await dbContext.MoneyAccounts.FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
