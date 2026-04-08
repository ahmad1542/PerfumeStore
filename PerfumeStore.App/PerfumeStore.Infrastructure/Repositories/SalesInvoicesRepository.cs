using Microsoft.EntityFrameworkCore;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;

namespace PerfumeStore.Infrastructure.Repositories {
    public class SalesInvoicesRepository(PerfumeStoreDbContext dbContext) : ISalesInvoicesRepository {
        public async Task<long> AddAsync(SalesInvoice salesInvoice, Dictionary<int, int> products) {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try {
                await dbContext.SalesInvoices.AddAsync(salesInvoice);
                await dbContext.SaveChangesAsync();

                foreach (var product in products) {
                    if (!await CheckIfProductExist(product.Key)) {
                        throw new NotFoundException(nameof(Product), product.Key.ToString());
                    }

                    await dbContext.SalesInvoiceItems.AddAsync(new SalesInvoiceItem {
                        SalesInvoiceID = salesInvoice.ID,
                        ProductID = product.Key,
                        Quantity = product.Value
                    });

                    await DecreaseInventoryAsync(product.Key, product.Value);
                }

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return salesInvoice.ID;
            } catch {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<SalesInvoice>> GetAllAsync(string? search = null, DateTime? fromDate = null, DateTime? toDate = null) {
            IQueryable<SalesInvoice> query = dbContext.SalesInvoices
                .Include(p => p.Customer)
                .Include(p => p.Debt)
                                .Include(p => p.SalesInvoiceItems);

            if (fromDate.HasValue) {
                var from = fromDate.Value.Date;
                query = query.Where(x => x.Date >= from);
            }

            if (toDate.HasValue) {
                var to = toDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(x => x.Date <= to);
            }

            if (!string.IsNullOrWhiteSpace(search)) {
                search = search.Trim();

                bool isLong = long.TryParse(search, out var searchId);
                bool isDecimal = decimal.TryParse(search, out var searchAmount);

                query = query.Where(x =>
                    (isLong && x.ID == searchId) ||
                    (isDecimal && x.AmountPaid == searchAmount) ||
                    (x.Customer != null && x.Customer.Name.Contains(search)) ||
                    (isDecimal && (
                        (x.Debt != null && x.Debt.Amount == searchAmount) ||
                        (searchAmount == 0 && x.Debt == null)
                    ))
                );
            }

            return await query.OrderByDescending(x => x.Date).ThenByDescending(x => x.ID).ToListAsync();
        }

        public async Task<SalesInvoice?> GetByIdAsync(long id) {
            var salesInvoice = await dbContext.SalesInvoices
                .Include(p => p.Customer)
                .Include(p => p.Debt)
                                .Include(p => p.SalesInvoiceItems).ThenInclude(i => i.Product).FirstOrDefaultAsync(s => s.ID == id);

            return salesInvoice;
        }

        public async Task UpdateProductsAsync(long salesInvoiceId, Dictionary<int, int> products) {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try {
                var oldItems = await dbContext.SalesInvoiceItems
                    .Where(x => x.SalesInvoiceID == salesInvoiceId)
                    .ToListAsync();

                // Return old sold quantities back to inventory
                foreach (var oldItem in oldItems) {
                    await IncreaseInventoryAsync(oldItem.ProductID, oldItem.Quantity);
                }

                dbContext.SalesInvoiceItems.RemoveRange(oldItems);

                foreach (var product in products) {
                    if (!await CheckIfProductExist(product.Key)) {
                        throw new NotFoundException(nameof(Product), product.Key.ToString());
                    }

                    await dbContext.SalesInvoiceItems.AddAsync(new SalesInvoiceItem {
                        SalesInvoiceID = salesInvoiceId,
                        ProductID = product.Key,
                        Quantity = product.Value
                    });

                    // Apply the new sale
                    await DecreaseInventoryAsync(product.Key, product.Value);
                }

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            } catch {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();

        private async Task<bool> CheckIfProductExist(int id) {
            return await dbContext.Products.AnyAsync(p => p.ID == id);
        }

        private async Task IncreaseInventoryAsync(int productId, int quantity) {
            var inventory = await dbContext.Inventory
                .FirstOrDefaultAsync(x => x.ProductID == productId);

            if (inventory == null) {
                await dbContext.Inventory.AddAsync(new Inventory {
                    ProductID = productId,
                    Quantity = quantity
                });
            } else {
                inventory.Quantity += quantity;
            }
        }

        private async Task DecreaseInventoryAsync(int productId, int quantity) {
            var inventory = await dbContext.Inventory
                .FirstOrDefaultAsync(x => x.ProductID == productId);

            if (inventory == null) {
                inventory = new Inventory {
                    ProductID = productId,
                    Quantity = 0
                };

                await dbContext.Inventory.AddAsync(inventory);
            }

            if (inventory.Quantity < quantity) {
                throw new Exception($"Not enough stock for product id {productId}. Available: {inventory.Quantity}, requested: {quantity}");
            }

            inventory.Quantity -= quantity;
        }
    }
}
