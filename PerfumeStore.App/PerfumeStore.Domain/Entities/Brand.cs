using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfumeStore.Domain.Entities;

public class Brand {
    public int ID { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string? BrandDescription { get; set; }

    public ICollection<Product> Products { get; private set; } = new List<Product>();
}
