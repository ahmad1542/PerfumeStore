using System;
using System.Collections.Generic;

namespace PerfumeStore.Domain.Entities;

public class PurchaseInvoice {
    public long PurchaseInvoiceID { get; set; }

    public DateTime InvoiceDate { get; set; } = DateTime.Now;

    public string? Notes { get; set; }

    public string SupplierPhone { get; set; } = null!;

    public Supplier Supplier { get; set; } = null!;

    public ICollection<PaymentVoucher> PaymentVouchers { get; private set; } = new List<PaymentVoucher>();

    public ICollection<PurchaseInvoiceItem> PurchaseInvoiceItems { get; private set; } = new List<PurchaseInvoiceItem>();
}
