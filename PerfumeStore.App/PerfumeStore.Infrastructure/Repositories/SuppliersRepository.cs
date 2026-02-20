using Microsoft.EntityFrameworkCore;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;

namespace PerfumeStore.Infrastructure.Repositories {
    public class SuppliersRepository(PerfumeStoreDbContext dbContext) : ISuppliersRepository {
        public async Task<Guid> AddAsync(Supplier supplier) {
            await dbContext.Suppliers.AddAsync(supplier);
            await dbContext.SaveChangesAsync();
            return supplier.Id;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync(string? search = null) {
            IQueryable<Supplier> query = dbContext.Suppliers;
            if (!string.IsNullOrWhiteSpace(search)) {
                query = query.Where(c => c.Name.Contains(search) || c.Phone.Contains(search));
            }
            return await query.ToListAsync();
        }

        public async Task<Supplier?> GetByIdAsync(Guid id) {
            return await dbContext.Suppliers
                                .Include(c => c.PaymentVouchers)
                                .Include(c => c.PurchaseInvoices)
                                .Include(c => c.Debts)
                                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
