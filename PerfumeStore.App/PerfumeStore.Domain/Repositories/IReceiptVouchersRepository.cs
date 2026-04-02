using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Domain.Repositories;

public interface IReceiptVouchersRepository {
    Task<long> AddAsync(ReceiptVoucher receiptVoucher, IEnumerable<ReceiptVoucherSalesInvoice> salesApplications, IEnumerable<ReceiptVoucherDebt> personDebtApplications);
    Task<IEnumerable<ReceiptVoucher>> GetAllAsync(string? search = null);
    Task<ReceiptVoucher?> GetByIdAsync(long id);
    Task<IEnumerable<SalesInvoice>> GetOpenSalesInvoicesAsync(Guid customerId);
    Task<IEnumerable<Debt>> GetOpenPersonDebtsAsync(Guid personId);
}
