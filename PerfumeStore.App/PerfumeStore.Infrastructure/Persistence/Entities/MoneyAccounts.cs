using System;
using System.Collections.Generic;

namespace PerfumeStore.Infrastructure.Persistence.Entities;

public partial class MoneyAccounts
{
    public int MoneyAccountID { get; set; }

    public string AccountName { get; set; } = null!;

    public decimal CurrentBalance { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<Expenses> Expenses { get; set; } = new List<Expenses>();

    public virtual ICollection<MoneyTransfers> MoneyTransfersFromMoneyAccount { get; set; } = new List<MoneyTransfers>();

    public virtual ICollection<MoneyTransfers> MoneyTransfersToMoneyAccount { get; set; } = new List<MoneyTransfers>();

    public virtual ICollection<PaymentVouchers> PaymentVouchers { get; set; } = new List<PaymentVouchers>();

    public virtual ICollection<ReceiptVouchers> ReceiptVouchers { get; set; } = new List<ReceiptVouchers>();
}
