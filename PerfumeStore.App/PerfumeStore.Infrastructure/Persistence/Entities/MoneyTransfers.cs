using System;
using System.Collections.Generic;

namespace PerfumeStore.Infrastructure.Persistence.Entities;

public partial class MoneyTransfers
{
    public long MoneyTransferID { get; set; }

    public DateTime TransferDate { get; set; }

    public int FromMoneyAccountID { get; set; }

    public int ToMoneyAccountID { get; set; }

    public decimal TransferAmount { get; set; }

    public string? Notes { get; set; }

    public virtual MoneyAccounts FromMoneyAccount { get; set; } = null!;

    public virtual MoneyAccounts ToMoneyAccount { get; set; } = null!;
}
