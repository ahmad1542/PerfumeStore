using Microsoft.EntityFrameworkCore;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;

namespace PerfumeStore.Infrastructure.Repositories {
    internal class BrandsRepository(PerfumeStoreDbContext dbContext) : IBrandsRepository {
        public async Task<int> AddAsync(Brand brand) {
            await dbContext.Brands.AddAsync(brand);
            await dbContext.SaveChangesAsync();
            return brand.ID;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync(string? search = null) {
            IQueryable<Brand> query = dbContext.Brands;

            if (!string.IsNullOrWhiteSpace(search)) {
                query = query.Where(b => b.Name.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<Brand?> GetByIdAsync(int id) {
            var brand = await dbContext.Brands.FirstOrDefaultAsync(s => s.ID == id);
            return brand;
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
