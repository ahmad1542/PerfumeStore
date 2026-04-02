using Microsoft.EntityFrameworkCore;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Repositories;
using PerfumeStore.Infrastructure.Persistence;

namespace PerfumeStore.Infrastructure.Repositories;

public class ReceiptVouchersRepository(PerfumeStoreDbContext dbContext) : IReceiptVouchersRepository {
    public async Task<long> AddAsync(ReceiptVoucher receiptVoucher, IEnumerable<ReceiptVoucherSalesInvoice> salesApplications, IEnumerable<ReceiptVoucherDebt> personDebtApplications) {
        var salesApplicationList = salesApplications.ToList();
        var personDebtApplicationList = personDebtApplications.ToList();

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try {
            await dbContext.ReceiptVouchers.AddAsync(receiptVoucher);
            await dbContext.SaveChangesAsync();

            foreach (var application in salesApplicationList) {
                var invoice = await dbContext.SalesInvoices
                    .Include(x => x.Debt)
                    .FirstOrDefaultAsync(x => x.ID == application.SalesInvoiceId);

                if (invoice == null) {
                    throw new Exception($"Sales invoice #{application.SalesInvoiceId} was not found.");
                }

                if (invoice.CustomerId != receiptVoucher.CustomerId) {
                    throw new Exception($"Sales invoice #{application.SalesInvoiceId} does not belong to the selected customer.");
                }

                if (invoice.Debt == null || invoice.Debt.IsDeleted || invoice.Debt.Amount <= 0) {
                    throw new Exception($"Sales invoice #{application.SalesInvoiceId} has no remaining debt.");
                }

                if (application.AppliedAmount > invoice.Debt.Amount) {
                    throw new Exception($"Applied amount for sales invoice #{application.SalesInvoiceId} exceeds its remaining debt.");
                }

                application.ReceiptVoucherId = receiptVoucher.ID;
                await dbContext.ReceiptVoucherSalesInvoices.AddAsync(application);

                invoice.Debt.Amount -= application.AppliedAmount;
                if (invoice.Debt.Amount <= 0) {
                    invoice.Debt.Amount = 0;
                    invoice.Debt.IsDeleted = true;
                }
            }

            foreach (var application in personDebtApplicationList) {
                var debt = await dbContext.Debts
                    .Include(x => x.Person)
                    .FirstOrDefaultAsync(x => x.Id == application.DebtId);

                if (debt == null) {
                    throw new Exception($"Debt #{application.DebtId} was not found.");
                }

                if (!debt.PersonId.HasValue || debt.PersonId != receiptVoucher.PersonId || debt.SalesInvoiceId.HasValue || debt.PurchaseInvoiceId.HasValue) {
                    throw new Exception($"Debt #{application.DebtId} does not belong to the selected person.");
                }

                if (debt.IsDeleted || debt.Amount <= 0) {
                    throw new Exception($"Debt #{application.DebtId} has no remaining balance.");
                }

                if (application.AppliedAmount > debt.Amount) {
                    throw new Exception($"Applied amount for debt #{application.DebtId} exceeds its remaining balance.");
                }

                application.ReceiptVoucherId = receiptVoucher.ID;
                await dbContext.ReceiptVoucherDebts.AddAsync(application);

                debt.Amount -= application.AppliedAmount;
                if (debt.Amount <= 0) {
                    debt.Amount = 0;
                    debt.IsDeleted = true;
                }
            }

            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return receiptVoucher.ID;
        } catch {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<IEnumerable<ReceiptVoucher>> GetAllAsync(string? search = null) {
        IQueryable<ReceiptVoucher> query = dbContext.ReceiptVouchers
            .Include(x => x.Customer)
            .Include(x => x.Person)
            .Include(x => x.MoneyAccount)
            .Include(x => x.AppliedSalesInvoices)
            .Include(x => x.AppliedPersonDebts);

        if (!string.IsNullOrWhiteSpace(search)) {
            search = search.Trim();
            var isLong = long.TryParse(search, out var searchId);
            var isInt = int.TryParse(search, out var searchDebtId);
            var isDecimal = decimal.TryParse(search, out var searchAmount);

            query = query.Where(x =>
                (isLong && x.ID == searchId) ||
                (isDecimal && x.Amount == searchAmount) ||
                (x.Customer != null && x.Customer.Name.Contains(search)) ||
                (x.Person != null && x.Person.Name.Contains(search)) ||
                (x.Notes != null && x.Notes.Contains(search)) ||
                x.AppliedSalesInvoices.Any(a => a.SalesInvoiceId.ToString().Contains(search)) ||
                (isInt && x.AppliedPersonDebts.Any(a => a.DebtId == searchDebtId))
            );
        }

        return await query.OrderByDescending(x => x.Date).ThenByDescending(x => x.ID).ToListAsync();
    }

    public async Task<ReceiptVoucher?> GetByIdAsync(long id) {
        return await dbContext.ReceiptVouchers
            .Include(x => x.Customer)
            .Include(x => x.Person)
            .Include(x => x.MoneyAccount)
            .Include(x => x.AppliedSalesInvoices)
                .ThenInclude(x => x.SalesInvoice)
            .Include(x => x.AppliedPersonDebts)
                .ThenInclude(x => x.Debt)
                    .ThenInclude(x => x.Person)
            .FirstOrDefaultAsync(x => x.ID == id);
    }

    public async Task<IEnumerable<SalesInvoice>> GetOpenSalesInvoicesAsync(Guid customerId) {
        return await dbContext.SalesInvoices
            .Include(x => x.Debt)
            .Where(x => x.CustomerId == customerId && x.Debt != null && !x.Debt.IsDeleted && x.Debt.Amount > 0)
            .OrderBy(x => x.Date)
            .ThenBy(x => x.ID)
            .ToListAsync();
    }

    public async Task<IEnumerable<Debt>> GetOpenPersonDebtsAsync(Guid personId) {
        return await dbContext.Debts
            .Include(x => x.Person)
            .Where(x => x.PersonId == personId && x.SalesInvoiceId == null && x.PurchaseInvoiceId == null && !x.IsDeleted && x.Amount > 0)
            .OrderBy(x => x.CreatedAt)
            .ThenBy(x => x.Id)
            .ToListAsync();
    }
}
