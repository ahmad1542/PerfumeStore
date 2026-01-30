using System;
using System.Collections.Generic;

namespace PerfumeStore.Infrastructure.Persistence.Entities;

public partial class PurchaseInvoiceItems
{
    public long PurchaseInvoiceItemID { get; set; }

    public int Quantity { get; set; }

    public decimal UnitCost { get; set; }

    public long PurchaseInvoiceID { get; set; }

    public int ProductID { get; set; }

    public virtual Products Product { get; set; } = null!;

    public virtual PurchaseInvoices PurchaseInvoice { get; set; } = null!;
}
