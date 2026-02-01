using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeStore.Domain.Entities;

public class PaymentVoucher {
    public long ID { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public decimal Amount { get; set; }

    public string? Notes { get; set; }

    public string SupplierPhone { get; set; } = null!;
    public long? PurchaseInvoiceID { get; set; }
    public int MoneyAccountID { get; set; }

    public MoneyAccount MoneyAccount { get; set; } = null!;
    public PurchaseInvoice? PurchaseInvoice { get; set; }
    public Supplier Supplier { get; set; } = null!;
}
