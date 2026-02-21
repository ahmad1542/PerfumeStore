using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeStore.Domain.Entities;

public class Inventory {
    [Key]
    public int ProductID { get; set; }

    public int Quantity { get; set; }

    public Product Product { get; set; } = null!;
}
