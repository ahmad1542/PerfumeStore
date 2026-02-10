using Microsoft.EntityFrameworkCore;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;

namespace PerfumeStore.Infrastructure.Repositories {
    public class ProductCategoriesRepository(PerfumeStoreDbContext dbContext) : IProductCategoriesRepository {
        public async Task<int> AddAsync(ProductCategory productCategory) {
            await dbContext.ProductCategories.AddAsync(productCategory);
            await dbContext.SaveChangesAsync();
            return productCategory.ID;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync(string? search = null) {
            IQueryable<ProductCategory> query = dbContext.ProductCategories;
            if (!string.IsNullOrEmpty(search)) {
                query = query.Where(pc => pc.Name.Contains(search));
            }
            return await query.ToListAsync();
        }

        public async Task<ProductCategory?> GetByIdAsync(int id) {
            var productCategory = await dbContext.ProductCategories.FirstOrDefaultAsync(s => s.ID == id);
            return productCategory;
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
