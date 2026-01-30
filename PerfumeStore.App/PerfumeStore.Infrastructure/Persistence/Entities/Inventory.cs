using System;
using System.Collections.Generic;

namespace PerfumeStore.Infrastructure.Persistence.Entities;

public partial class Inventory
{
    public int ProductID { get; set; }

    public int Quantity { get; set; }

    public virtual Products Product { get; set; } = null!;
}
