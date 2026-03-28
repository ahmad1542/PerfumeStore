using Microsoft.EntityFrameworkCore;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;

namespace PerfumeStore.Infrastructure.Repositories {
    public class ExpenseTypesRepository(PerfumeStoreDbContext dbContext) : IExpenseTypesRepository {
        public async Task<int> AddAsync(ExpenseType expenseType) {
            await dbContext.Set<ExpenseType>().AddAsync(expenseType);
            await dbContext.SaveChangesAsync();
            return expenseType.Id;
        }

        public async Task<IEnumerable<ExpenseType>> GetAllAsync(string? search = null) {
            IQueryable<ExpenseType> query = dbContext.Set<ExpenseType>().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search)) {
                search = search.Trim();
                query = query.Where(x => x.Name.Contains(search));
            }

            return await query.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<ExpenseType?> GetByIdAsync(int id) {
            return await dbContext.Set<ExpenseType>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
