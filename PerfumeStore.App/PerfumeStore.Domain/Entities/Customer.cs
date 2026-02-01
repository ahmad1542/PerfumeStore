using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Domain.Entities;

public class Customer : Person {

    public ICollection<ReceiptVoucher> ReceiptVouchers { get; private set; } = new List<ReceiptVoucher>();

    public ICollection<SalesInvoice> SalesInvoices { get; private set; } = new List<SalesInvoice>();
}
