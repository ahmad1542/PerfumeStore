using PerfumeStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerfumeStore.Domain.Repositories {
    public interface IBrandsRepository {
        Task<Brand?> GetByIdAsync(int id);
        Task<IEnumerable<Brand>> GetAllAsync(string? search = null);
        Task<int> AddAsync(Brand brand);
        Task Update(Brand brand);
        Task SaveChangesAsync();
    }
}
