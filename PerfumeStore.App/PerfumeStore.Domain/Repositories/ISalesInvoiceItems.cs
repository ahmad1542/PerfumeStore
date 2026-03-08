using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Domain.Repositories {
    public interface ISalesInvoiceItems {
        Task<SalesInvoiceItem?> GetByIdAsync(long id);
        Task<IEnumerable<SalesInvoiceItem>> GetAllAsync(string? search = null);
        Task<long> AddAsync(SalesInvoiceItem salesInvoiceItem);
        Task SaveChangesAsync();
    }
}
