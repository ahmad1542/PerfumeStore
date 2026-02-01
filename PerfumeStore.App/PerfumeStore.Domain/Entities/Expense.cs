using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Domain.Entities;

public class Expense {
    public long ID { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    [Required]
    public string Type { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? Notes { get; set; }

    public int MoneyAccountID { get; set; }

    public MoneyAccount MoneyAccount { get; set; } = null!;
}
