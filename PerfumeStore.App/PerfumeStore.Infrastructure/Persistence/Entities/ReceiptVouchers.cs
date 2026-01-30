using System;
using System.Collections.Generic;

namespace PerfumeStore.Infrastructure.Persistence.Entities;

public partial class ReceiptVouchers
{
    public long ReceiptVoucherID { get; set; }

    public DateTime ReceiptVoucherDate { get; set; }

    public decimal Amount { get; set; }

    public string? Notes { get; set; }

    public string CustomerPhone { get; set; } = null!;

    public long? SalesInvoiceID { get; set; }

    public int MoneyAccountID { get; set; }

    public virtual Customers CustomerPhoneNavigation { get; set; } = null!;

    public virtual MoneyAccounts MoneyAccount { get; set; } = null!;

    public virtual SalesInvoices? SalesInvoice { get; set; }
}
