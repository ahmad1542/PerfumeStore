using PerfumeStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerfumeStore.Domain.Repositories {
    public interface IBrandsRepository {
        Task<Brand?> GetByIdAsync(int id);
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<int> AddAsync(Brand brand);
        Task SaveChangesAsync();
    }
}
