using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Domain.Repositories {
    public interface ISalesInvoicesRepository {
        Task<SalesInvoice?> GetByIdAsync(long id);
        Task<IEnumerable<SalesInvoice>> GetAllAsync();
        Task<long> AddAsync(SalesInvoice salesInvoice, Dictionary<int, int> products);
        Task SaveChangesAsync();
    }
}
