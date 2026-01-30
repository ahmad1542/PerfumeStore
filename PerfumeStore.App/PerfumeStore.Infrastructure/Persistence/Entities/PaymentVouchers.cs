using System;
using System.Collections.Generic;

namespace PerfumeStore.Infrastructure.Persistence.Entities;

public partial class PaymentVouchers
{
    public long PaymentVoucherID { get; set; }

    public DateTime PaymentVoucherDate { get; set; }

    public decimal Amount { get; set; }

    public string? Notes { get; set; }

    public string SupplierPhone { get; set; } = null!;

    public long? PurchaseInvoiceID { get; set; }

    public int MoneyAccountID { get; set; }

    public virtual MoneyAccounts MoneyAccount { get; set; } = null!;

    public virtual PurchaseInvoices? PurchaseInvoice { get; set; }

    public virtual Suppliers SupplierPhoneNavigation { get; set; } = null!;
}
