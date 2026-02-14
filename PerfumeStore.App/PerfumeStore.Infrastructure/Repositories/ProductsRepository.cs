using Microsoft.EntityFrameworkCore;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;

namespace PerfumeStore.Infrastructure.Repositories {
    public class ProductsRepository(PerfumeStoreDbContext dbContext) : IProductsRepository {
        public async Task<int> AddAsync(Product product) {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product.ID;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(string? search = null) {
            IQueryable<Product> query = dbContext.Products.Include(p => p.Brand).Include(p => p.ProductCategory);
            if (!string.IsNullOrEmpty(search)) {
                query = query.Where(p => p.Name.Contains(search));
            }
            return await query.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id) {
            var product = await dbContext.Products.Include(p => p.Brand).Include(p => p.ProductCategory).FirstOrDefaultAsync(s => s.ID == id);
            return product;
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
