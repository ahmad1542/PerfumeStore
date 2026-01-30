using System;
using System.Collections.Generic;

namespace PerfumeStore.Infrastructure.Persistence.Entities;

public partial class SalesInvoices
{
    public long SalesInvoiceID { get; set; }

    public DateTime InvoiceDate { get; set; }

    public string? Notes { get; set; }

    public string? CustomerPhone { get; set; }

    public virtual Customers? CustomerPhoneNavigation { get; set; }

    public virtual ICollection<ReceiptVouchers> ReceiptVouchers { get; set; } = new List<ReceiptVouchers>();

    public virtual ICollection<SalesInvoiceItems> SalesInvoiceItems { get; set; } = new List<SalesInvoiceItems>();
}
