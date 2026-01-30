using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Domain.Entities;

public class Inventory {
    [Key]
    public int ProductID { get; set; }

    public int Quantity { get; set; }

    public Product Product { get; set; } = null!;
}
