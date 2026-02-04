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

        public async Task<IEnumerable<Brand>> GetAllAsync() {
            var brands = await dbContext.Brands.ToListAsync();
            return brands;
        }

        public async Task<Brand?> GetByIdAsync(int id) {
            var brand = await dbContext.Brands.FirstOrDefaultAsync(s => s.ID == id);
            return brand;
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
