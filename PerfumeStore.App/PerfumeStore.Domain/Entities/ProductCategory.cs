using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Domain.Entities;

public class ProductCategory {
    public int ProductCategoryID { get; set; }

    [Required]
    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public ICollection<Product> Products { get; private set; } = new List<Product>();
}
