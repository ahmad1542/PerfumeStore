using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Domain.Entities;

public class Supplier : Person {

    public ICollection<PaymentVoucher> PaymentVouchers { get; private set; } = new List<PaymentVoucher>();
    public ICollection<PurchaseInvoice> PurchaseInvoices { get; private set; } = new List<PurchaseInvoice>();
}
