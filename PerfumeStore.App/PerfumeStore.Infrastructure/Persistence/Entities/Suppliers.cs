using System;
using System.Collections.Generic;

namespace PerfumeStore.Infrastructure.Persistence.Entities;

public partial class Suppliers
{
    public string SupplierPhone { get; set; } = null!;

    public string SupplierName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<PaymentVouchers> PaymentVouchers { get; set; } = new List<PaymentVouchers>();

    public virtual ICollection<PurchaseInvoices> PurchaseInvoices { get; set; } = new List<PurchaseInvoices>();
}
