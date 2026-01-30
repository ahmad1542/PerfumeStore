using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Domain.Entities;

public class Supplier {
    public string SupplierPhone { get; set; } = null!;

    [Required]
    public string SupplierName { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<PaymentVoucher> PaymentVouchers { get; private set; } = new List<PaymentVoucher>();
    public ICollection<PurchaseInvoice> PurchaseInvoices { get; private set; } = new List<PurchaseInvoice>();
}
