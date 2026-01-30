using System;
using System.Collections.Generic;

namespace PerfumeStore.Infrastructure.Persistence.Entities;

public partial class Customers
{
    public string CustomerPhone { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ReceiptVouchers> ReceiptVouchers { get; set; } = new List<ReceiptVouchers>();

    public virtual ICollection<SalesInvoices> SalesInvoices { get; set; } = new List<SalesInvoices>();
}
