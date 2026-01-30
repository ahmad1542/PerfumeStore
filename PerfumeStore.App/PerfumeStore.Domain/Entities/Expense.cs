using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Domain.Entities;

public class Expense {
    public long ExpenseID { get; set; }

    public DateTime ExpenseDate { get; set; } = DateTime.Now;

    [Required]
    public string ExpenseType { get; set; } = null!;

    public decimal ExpenseAmount { get; set; }

    public string? Notes { get; set; }

    public int MoneyAccountID { get; set; }

    public MoneyAccount MoneyAccount { get; set; } = null!;
}
