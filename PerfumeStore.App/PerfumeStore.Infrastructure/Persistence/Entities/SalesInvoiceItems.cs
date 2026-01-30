using System;
using System.Collections.Generic;

namespace PerfumeStore.Infrastructure.Persistence.Entities;

public partial class SalesInvoiceItems
{
    public long SalesInvoiceItemID { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public long SalesInvoiceID { get; set; }

    public int ProductID { get; set; }

    public virtual Products Product { get; set; } = null!;

    public virtual SalesInvoices SalesInvoice { get; set; } = null!;
}
