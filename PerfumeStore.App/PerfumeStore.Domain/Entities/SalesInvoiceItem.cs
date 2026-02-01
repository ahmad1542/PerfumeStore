using System;
using System.Collections.Generic;

namespace PerfumeStore.Domain.Entities;

public class SalesInvoiceItem {
    public long ID { get; set; }

    public int Quantity { get; set; }

    public long SalesInvoiceID { get; set; }

    public int ProductID { get; set; }

    public Product Product { get; set; } = null!;

    public SalesInvoice SalesInvoice { get; set; } = null!;
}
