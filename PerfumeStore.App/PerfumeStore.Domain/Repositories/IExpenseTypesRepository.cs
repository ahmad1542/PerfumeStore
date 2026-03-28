using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Domain.Repositories {
    public interface IExpenseTypesRepository {
        Task<ExpenseType?> GetByIdAsync(int id);
        Task<IEnumerable<ExpenseType>> GetAllAsync(string? search = null);
        Task<int> AddAsync(ExpenseType expenseType);
        Task SaveChangesAsync();
    }
}
