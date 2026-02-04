using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Domain.Entities;

public class MoneyAccount {
    public int ID { get; set; }

    [Required]
    public string AccountName { get; set; } = null!;

    [Range(0, double.MaxValue)]
    public decimal CurrentBalance { get; set; }

    public string? Notes { get; set; }

    public ICollection<Expense> Expenses { get; private set; } = new List<Expense>();

    public ICollection<MoneyTransaction> MoneyTransfersFromMoneyAccount { get; private set; } = new List<MoneyTransaction>();

    public ICollection<MoneyTransaction> MoneyTransfersToMoneyAccount { get; private set; } = new List<MoneyTransaction>();

    public ICollection<PaymentVoucher> PaymentVouchers { get; private set; } = new List<PaymentVoucher>();

    public ICollection<ReceiptVoucher> ReceiptVouchers { get; private set; } = new List<ReceiptVoucher>();
}
