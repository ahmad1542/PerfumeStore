using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Domain.Entities;

public class Customer {
    [Key]
    public string CustomerPhone { get; set; } = null!;

    [Required]
    public string CustomerName { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<ReceiptVoucher> ReceiptVouchers { get; private set; } = new List<ReceiptVoucher>();

    public ICollection<SalesInvoice> SalesInvoices { get; private set; } = new List<SalesInvoice>();
}
