using System;
using System.Collections.Generic;

namespace PerfumeStore.Infrastructure.Persistence.Entities;

public partial class Expenses
{
    public long ExpenseID { get; set; }

    public DateTime ExpenseDate { get; set; }

    public string ExpenseType { get; set; } = null!;

    public decimal ExpenseAmount { get; set; }

    public string? Notes { get; set; }

    public int MoneyAccountID { get; set; }

    public virtual MoneyAccounts MoneyAccount { get; set; } = null!;
}
