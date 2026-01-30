using System;
using System.Collections.Generic;

namespace PerfumeStore.Infrastructure.Persistence.Entities;

public partial class Products
{
    public int ProductID { get; set; }

    public string ProductName { get; set; } = null!;

    public int Capacity { get; set; }

    public decimal SalePrice { get; set; }

    public decimal CostPrice { get; set; }

    public int? MinStock { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? BrandID { get; set; }

    public int ProductCategoryID { get; set; }

    public virtual Brands? Brand { get; set; }

    public virtual Inventory? Inventory { get; set; }

    public virtual ProductCategories ProductCategory { get; set; } = null!;

    public virtual ICollection<PurchaseInvoiceItems> PurchaseInvoiceItems { get; set; } = new List<PurchaseInvoiceItems>();

    public virtual ICollection<SalesInvoiceItems> SalesInvoiceItems { get; set; } = new List<SalesInvoiceItems>();
}
