using Microsoft.EntityFrameworkCore;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;

namespace PerfumeStore.Infrastructure.Repositories {
    public class SalesInvoicesRepository(PerfumeStoreDbContext dbContext) : ISalesInvoicesRepository {
        public async Task<long> AddAsync(SalesInvoice salesInvoice, Dictionary<int, int> products) {
            await dbContext.SalesInvoices.AddAsync(salesInvoice);
            await dbContext.SaveChangesAsync();
            foreach (var product in products) {
                if (!await CheckIfProductExist(product.Key)) {
                    throw new NotFoundException(nameof(Product), product.Key.ToString());
                }
                await dbContext.SalesInvoiceItems.AddAsync(new SalesInvoiceItem {
                    SalesInvoiceID = salesInvoice.ID,
                    ProductID = product.Key,
                    Quantity = product.Value
                });
            }

            await dbContext.SaveChangesAsync();
            return salesInvoice.ID;
        }

        public async Task<IEnumerable<SalesInvoice>> GetAllAsync() {
            IQueryable<SalesInvoice> query = dbContext.SalesInvoices.Include(p => p.Customer).Include(p => p.Debt).Include(p => p.ReceiptVouchers).Include(p => p.SalesInvoiceItems);
            return await query.ToListAsync();
        }

        public async Task<SalesInvoice?> GetByIdAsync(long id) {
            var salesInvoice = await dbContext.SalesInvoices.Include(p => p.Customer).Include(p => p.Debt).Include(p => p.ReceiptVouchers).Include(p => p.SalesInvoiceItems).FirstOrDefaultAsync(s => s.ID == id);
            return salesInvoice;
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();

        private async Task<bool> CheckIfProductExist(int id) {
            return await dbContext.Products.AnyAsync(p => p.ID == id);
        }
    }
}
