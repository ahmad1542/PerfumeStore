using System;
using System.Collections.Generic;

namespace PerfumeStore.Domain.Entities;

public class SalesInvoice {
    public long ID { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public string? Notes { get; set; }

    public string? CustomerPhone { get; set; }

    public Customer? Customer { get; set; }
    public int AmountPaid {  get; set; }

    public Debt? Debt { get; set; }
    public ICollection<ReceiptVoucher> ReceiptVouchers { get; private set; } = new List<ReceiptVoucher>();

    public ICollection<SalesInvoiceItem> SalesInvoiceItems { get; private set; } = new List<SalesInvoiceItem>();
}
