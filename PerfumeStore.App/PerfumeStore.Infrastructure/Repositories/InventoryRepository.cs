using Microsoft.EntityFrameworkCore;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;

namespace PerfumeStore.Infrastructure.Repositories {
    public class InventoryRepository(PerfumeStoreDbContext dbContext) : IInventoryRepository {
        public async Task<int> AddAsync(Inventory inventory) {
            await dbContext.Inventory.AddAsync(inventory);
            await dbContext.SaveChangesAsync();
            return inventory.ProductID;
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync(string? search = null) {
            IQueryable<Inventory> query = dbContext.Inventory;
            if (!string.IsNullOrWhiteSpace(search)) {
                query = query.Where(c => c.Product.Name.Contains(search));
            }
            return await query.ToListAsync();
        }

        public async Task<Inventory?> GetByIdAsync(int id) {
            return await dbContext.Inventory
                                .Include(c => c.Product)
                                .FirstOrDefaultAsync(c => c.ProductID == id);
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
