using System;
using System.Collections.Generic;

namespace PerfumeStore.Infrastructure.Persistence.Entities;

public partial class PurchaseInvoices
{
    public long PurchaseInvoiceID { get; set; }

    public DateTime InvoiceDate { get; set; }

    public string? Notes { get; set; }

    public string SupplierPhone { get; set; } = null!;

    public virtual ICollection<PaymentVouchers> PaymentVouchers { get; set; } = new List<PaymentVouchers>();

    public virtual ICollection<PurchaseInvoiceItems> PurchaseInvoiceItems { get; set; } = new List<PurchaseInvoiceItems>();

    public virtual Suppliers SupplierPhoneNavigation { get; set; } = null!;
}
