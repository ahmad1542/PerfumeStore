using System;
using System.Collections.Generic;

namespace PerfumeStore.Domain.Entities;

public class MoneyTransaction {
    public long ID { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public int FromMoneyAccountID { get; set; }

    public int ToMoneyAccountID { get; set; }

    public decimal TransferAmount { get; set; }

    public string? Notes { get; set; }

    public MoneyAccount FromMoneyAccount { get; set; } = null!;

    public MoneyAccount ToMoneyAccount { get; set; } = null!;
}
