using Microsoft.EntityFrameworkCore;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;

namespace PerfumeStore.Infrastructure.Repositories {
    public class PersonsRepository(PerfumeStoreDbContext dbContext) : IPersonsRepository {
        public async Task<Guid> AddAsync(Person person) {
            await dbContext.Persons.AddAsync(person);
            await dbContext.SaveChangesAsync();
            return person.Id;
        }

        public async Task<IEnumerable<Person>> GetAllAsync(string? search = null) {
            IQueryable<Person> query = dbContext.Persons;
            if (!string.IsNullOrWhiteSpace(search)) {
                query = query.Where(p => p.Name.Contains(search) || p.Phone.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(Guid id) {
            var person = await dbContext.Persons.Include(p => p.Debts).FirstOrDefaultAsync(s => s.Id == id);
            return person;
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
