using Microsoft.EntityFrameworkCore;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;

namespace PerfumeStore.Infrastructure.Repositories {
    public class DebtsRepository(PerfumeStoreDbContext dbContext) : IDebtsRepository {
        public async Task<int> AddAsync(Debt debt) {
            await dbContext.Debts.AddAsync(debt);
            await dbContext.SaveChangesAsync();
            return debt.Id;
        }

        public async Task<IEnumerable<Debt>> GetAllAsync(string? search = null) {
            IQueryable<Debt> query = dbContext.Debts.Include(d => d.Person).Include(d => d.SalesInvoice).Include(d => d.PurchaseInvoice);

            if (!string.IsNullOrWhiteSpace(search)) {
                search = search.Trim();

                query = query.Where(d =>
                    d.Id.ToString().Contains(search) ||
                    d.Amount.ToString().Contains(search) ||
                    (d.Notes != null && d.Notes.Contains(search)) ||
                    (d.SalesInvoice != null && d.SalesInvoice.ID.ToString().Contains(search)) ||
                    (d.PurchaseInvoice != null && d.PurchaseInvoice.ID.ToString().Contains(search)) ||
                    (d.Person != null && d.Person.Name.Contains(search)) ||
                    (d.Person != null && d.Person.Phone.Contains(search))
                );
            }

            return await query.ToListAsync();
        }

        public async Task<Debt?> GetByIdAsync(int id) {
            var debt = await dbContext.Debts.FirstOrDefaultAsync(s => s.Id == id);
            return debt;
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
