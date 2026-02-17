using Microsoft.EntityFrameworkCore;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;

namespace PerfumeStore.Infrastructure.Repositories {
    public class CustomersRepository(PerfumeStoreDbContext dbContext) : ICustomersRepository {
        public async Task<string> AddAsync(Customer customer) {
            await dbContext.Customers.AddAsync(customer);
            return customer.Phone;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(string? search = null) {
            IQueryable<Customer> query = dbContext.Customers;
            if (!string.IsNullOrWhiteSpace(search)) {
                query = query.Where(c => c.Name.Contains(search) || c.Phone.Contains(search));
            }
            return await query.ToListAsync();
        }

        public async Task<Customer?> GetByPhoneNoAsync(string phone) {
            return await dbContext.Customers.Include(c => c.ReceiptVouchers)
                                .Include(c => c.SalesInvoices)
                                .Include(c => c.Debts)
                                .FirstOrDefaultAsync(c => c.Phone == phone);
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
