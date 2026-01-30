using System;
using System.Collections.Generic;

namespace PerfumeStore.Infrastructure.Persistence.Entities;

public partial class ProductCategories
{
    public int ProductCategoryID { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Products> Products { get; set; } = new List<Products>();
}
