using Microsoft.EntityFrameworkCore;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;

namespace PerfumeStore.Infrastructure.Repositories {
    public class PurchaseInvoicesRepository(PerfumeStoreDbContext dbContext) : IPurchaseInvoicesRepository {
        public async Task<long> AddAsync(PurchaseInvoice purchaseInvoice, Dictionary<int, int> products) {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try {
                await dbContext.PurchaseInvoices.AddAsync(purchaseInvoice);
                await dbContext.SaveChangesAsync();

                foreach (var product in products) {
                    if (!await CheckIfProductExist(product.Key)) {
                        throw new NotFoundException(nameof(Product), product.Key.ToString());
                    }

                    await dbContext.PurchaseInvoiceItems.AddAsync(new PurchaseInvoiceItem {
                        PurchaseInvoiceID = purchaseInvoice.ID,
                        ProductID = product.Key,
                        Quantity = product.Value
                    });

                    await IncreaseInventoryAsync(product.Key, product.Value);
                }

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return purchaseInvoice.ID;
            } catch {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<PurchaseInvoice>> GetAllAsync(string? search = null, DateTime? fromDate = null, DateTime? toDate = null) {
            IQueryable<PurchaseInvoice> query = dbContext.PurchaseInvoices
                .Include(p => p.Supplier)
                .Include(p => p.Debt)
                .Include(p => p.PurchaseInvoiceItems);

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
                    (x.Supplier != null && x.Supplier.Name.Contains(search)) ||
                    (isDecimal && (
                        (x.Debt != null && x.Debt.Amount == searchAmount) ||
                        (searchAmount == 0 && x.Debt == null)
                    ))
                );
            }

            return await query.ToListAsync();
        }

        public async Task<PurchaseInvoice?> GetByIdAsync(long id) {
            var purchaseInvoice = await dbContext.PurchaseInvoices
                .Include(p => p.Supplier)
                .Include(p => p.Debt)
                                .Include(p => p.PurchaseInvoiceItems).ThenInclude(i => i.Product).FirstOrDefaultAsync(s => s.ID == id);

            return purchaseInvoice;
        }

        public async Task UpdateProductsAsync(long purchaseInvoiceId, Dictionary<int, int> products) {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try {
                var oldItems = await dbContext.PurchaseInvoiceItems
                    .Where(x => x.PurchaseInvoiceID == purchaseInvoiceId)
                    .ToListAsync();

                // Remove old purchased quantities from inventory first
                foreach (var oldItem in oldItems) {
                    await DecreaseInventoryForPurchaseUpdateAsync(oldItem.ProductID, oldItem.Quantity);
                }

                dbContext.PurchaseInvoiceItems.RemoveRange(oldItems);

                foreach (var product in products) {
                    if (!await CheckIfProductExist(product.Key)) {
                        throw new NotFoundException(nameof(Product), product.Key.ToString());
                    }

                    await dbContext.PurchaseInvoiceItems.AddAsync(new PurchaseInvoiceItem {
                        PurchaseInvoiceID = purchaseInvoiceId,
                        ProductID = product.Key,
                        Quantity = product.Value
                    });

                    // Apply the new purchase
                    await IncreaseInventoryAsync(product.Key, product.Value);
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

        private async Task DecreaseInventoryForPurchaseUpdateAsync(int productId, int quantity) {
            var inventory = await dbContext.Inventory
                .FirstOrDefaultAsync(x => x.ProductID == productId);

            if (inventory == null) {
                throw new Exception($"Inventory record not found for product id {productId}.");
            }

            if (inventory.Quantity < quantity) {
                throw new Exception($"Cannot reverse old purchase invoice for product id {productId}. Available: {inventory.Quantity}, required: {quantity}");
            }

            inventory.Quantity -= quantity;
        }
    }
}
