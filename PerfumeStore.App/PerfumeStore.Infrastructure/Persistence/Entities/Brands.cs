using System;
using System.Collections.Generic;

namespace PerfumeStore.Infrastructure.Persistence.Entities;

public partial class Brands
{
    public int BrandID { get; set; }

    public string BrandName { get; set; } = null!;

    public string? BrandDescription { get; set; }

    public virtual ICollection<Products> Products { get; set; } = new List<Products>();
}
