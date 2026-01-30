using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Domain.Entities;

public class Brand {
    public int BrandID { get; set; }

    [Required]
    public string BrandName { get; set; } = null!;

    public string? BrandDescription { get; set; }

    public ICollection<Product> Products { get; private set; } = new List<Product>();
}
