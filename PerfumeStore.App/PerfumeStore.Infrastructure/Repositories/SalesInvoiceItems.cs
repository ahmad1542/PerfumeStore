using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;

namespace PerfumeStore.Infrastructure.Repositories {
    public class SalesInvoiceItems(PerfumeStoreDbContext dbContext) : ISalesInvoiceItems {
        public async Task<long> AddAsync(SalesInvoiceItem salesInvoiceItem) {
            
        }

        public async Task<IEnumerable<SalesInvoiceItem>> GetAllAsync(string? search = null) {
            throw new NotImplementedException();
        }

        public async Task<SalesInvoiceItem?> GetByIdAsync(long id) {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync() {
            throw new NotImplementedException();
        }
    }
}
