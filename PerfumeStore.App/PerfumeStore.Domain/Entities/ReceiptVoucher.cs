using System;
using System.Collections.Generic;

namespace PerfumeStore.Domain.Entities;

public class ReceiptVoucher {
    public long ReceiptVoucherID { get; set; }

    public DateTime ReceiptVoucherDate { get; set; } = DateTime.Now;

    public decimal Amount { get; set; }

    public string? Notes { get; set; }

    public string CustomerPhone { get; set; } = null!;

    public long? SalesInvoiceID { get; set; }

    public int MoneyAccountID { get; set; }

    public Customer Customer { get; set; } = null!;

    public MoneyAccount MoneyAccount { get; set; } = null!;

    public SalesInvoice? SalesInvoice { get; set; }
}
