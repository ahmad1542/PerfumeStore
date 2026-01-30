using Microsoft.EntityFrameworkCore;
using PerfumeStore.Infrastructure.Persistence.Entities;

namespace PerfumeStore.Infrastructure.Persistence;

public partial class PerfumeStoreDbContext : DbContext
{
    public PerfumeStoreDbContext()
    {
    }

    public PerfumeStoreDbContext(DbContextOptions<PerfumeStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brands> Brands { get; set; }

    public virtual DbSet<Customers> Customers { get; set; }

    public virtual DbSet<Expenses> Expenses { get; set; }

    public virtual DbSet<Inventory> Inventory { get; set; }

    public virtual DbSet<MoneyAccounts> MoneyAccounts { get; set; }

    public virtual DbSet<MoneyTransfers> MoneyTransfers { get; set; }

    public virtual DbSet<PaymentVouchers> PaymentVouchers { get; set; }

    public virtual DbSet<ProductCategories> ProductCategories { get; set; }

    public virtual DbSet<Products> Products { get; set; }

    public virtual DbSet<PurchaseInvoiceItems> PurchaseInvoiceItems { get; set; }

    public virtual DbSet<PurchaseInvoices> PurchaseInvoices { get; set; }

    public virtual DbSet<ReceiptVouchers> ReceiptVouchers { get; set; }

    public virtual DbSet<SalesInvoiceItems> SalesInvoiceItems { get; set; }

    public virtual DbSet<SalesInvoices> SalesInvoices { get; set; }

    public virtual DbSet<Suppliers> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brands>(entity =>
        {
            entity.HasKey(e => e.BrandID);

            entity.HasIndex(e => e.BrandName, "IX_Brands").IsUnique();

            entity.Property(e => e.BrandDescription)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.BrandName).HasMaxLength(150);
        });

        modelBuilder.Entity<Customers>(entity =>
        {
            entity.HasKey(e => e.CustomerPhone);

            entity.Property(e => e.CustomerPhone).HasMaxLength(30);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_Customers_CreatedAt");
            entity.Property(e => e.CustomerName).HasMaxLength(150);
        });

        modelBuilder.Entity<Expenses>(entity =>
        {
            entity.HasKey(e => e.ExpenseID);

            entity.Property(e => e.ExpenseAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExpenseDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_Expenses_ExpenseDate");
            entity.Property(e => e.ExpenseType).HasMaxLength(150);
            entity.Property(e => e.Notes).HasMaxLength(250);

            entity.HasOne(d => d.MoneyAccount).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.MoneyAccountID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Expenses_MoneyAccounts1");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.ProductID);

            entity.Property(e => e.ProductID).ValueGeneratedNever();

            entity.HasOne(d => d.Product).WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(d => d.ProductID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_Products1");
        });

        modelBuilder.Entity<MoneyAccounts>(entity =>
        {
            entity.HasKey(e => e.MoneyAccountID);

            entity.Property(e => e.AccountName).HasMaxLength(50);
            entity.Property(e => e.CurrentBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Notes).HasMaxLength(250);
        });

        modelBuilder.Entity<MoneyTransfers>(entity =>
        {
            entity.HasKey(e => e.MoneyTransferID);

            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.TransferAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransferDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_MoneyTransfers_TransferDate");

            entity.HasOne(d => d.FromMoneyAccount).WithMany(p => p.MoneyTransfersFromMoneyAccount)
                .HasForeignKey(d => d.FromMoneyAccountID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MoneyTransfers_MoneyAccounts1");

            entity.HasOne(d => d.ToMoneyAccount).WithMany(p => p.MoneyTransfersToMoneyAccount)
                .HasForeignKey(d => d.ToMoneyAccountID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MoneyTransfers_MoneyAccounts");
        });

        modelBuilder.Entity<PaymentVouchers>(entity =>
        {
            entity.HasKey(e => e.PaymentVoucherID);

            entity.Property(e => e.PaymentVoucherID).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.PaymentVoucherDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_PaymentVouchers_PaymentVoucherDate");
            entity.Property(e => e.SupplierPhone).HasMaxLength(30);

            entity.HasOne(d => d.MoneyAccount).WithMany(p => p.PaymentVouchers)
                .HasForeignKey(d => d.MoneyAccountID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentVouchers_MoneyAccounts1");

            entity.HasOne(d => d.PurchaseInvoice).WithMany(p => p.PaymentVouchers)
                .HasForeignKey(d => d.PurchaseInvoiceID)
                .HasConstraintName("FK_PaymentVouchers_PurchaseInvoices1");

            entity.HasOne(d => d.SupplierPhoneNavigation).WithMany(p => p.PaymentVouchers)
                .HasForeignKey(d => d.SupplierPhone)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentVouchers_Suppliers1");
        });

        modelBuilder.Entity<ProductCategories>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryID);

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(250);
        });

        modelBuilder.Entity<Products>(entity =>
        {
            entity.HasKey(e => e.ProductID);

            entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt).HasPrecision(0);
            entity.Property(e => e.ProductName).HasMaxLength(150);
            entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandID)
                .HasConstraintName("FK_Products_Brands1");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_ProductCategories1");
        });

        modelBuilder.Entity<PurchaseInvoiceItems>(entity =>
        {
            entity.HasKey(e => e.PurchaseInvoiceItemID);

            entity.Property(e => e.UnitCost).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseInvoiceItems)
                .HasForeignKey(d => d.ProductID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseInvoiceItems_Products1");

            entity.HasOne(d => d.PurchaseInvoice).WithMany(p => p.PurchaseInvoiceItems)
                .HasForeignKey(d => d.PurchaseInvoiceID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseInvoiceItems_PurchaseInvoices1");
        });

        modelBuilder.Entity<PurchaseInvoices>(entity =>
        {
            entity.HasKey(e => e.PurchaseInvoiceID);

            entity.Property(e => e.InvoiceDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_PurchaseInvoices_InvoiceDate");
            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.SupplierPhone).HasMaxLength(30);

            entity.HasOne(d => d.SupplierPhoneNavigation).WithMany(p => p.PurchaseInvoices)
                .HasForeignKey(d => d.SupplierPhone)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseInvoices_Suppliers1");
        });

        modelBuilder.Entity<ReceiptVouchers>(entity =>
        {
            entity.HasKey(e => e.ReceiptVoucherID);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CustomerPhone).HasMaxLength(30);
            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.ReceiptVoucherDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_ReceiptVouchers_ReceiptVoucherDate");

            entity.HasOne(d => d.CustomerPhoneNavigation).WithMany(p => p.ReceiptVouchers)
                .HasForeignKey(d => d.CustomerPhone)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReceiptVouchers_Customers1");

            entity.HasOne(d => d.MoneyAccount).WithMany(p => p.ReceiptVouchers)
                .HasForeignKey(d => d.MoneyAccountID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReceiptVouchers_MoneyAccounts1");

            entity.HasOne(d => d.SalesInvoice).WithMany(p => p.ReceiptVouchers)
                .HasForeignKey(d => d.SalesInvoiceID)
                .HasConstraintName("FK_ReceiptVouchers_SalesInvoices1");
        });

        modelBuilder.Entity<SalesInvoiceItems>(entity =>
        {
            entity.HasKey(e => e.SalesInvoiceItemID);

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.SalesInvoiceItems)
                .HasForeignKey(d => d.ProductID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesInvoiceItems_Products1");

            entity.HasOne(d => d.SalesInvoice).WithMany(p => p.SalesInvoiceItems)
                .HasForeignKey(d => d.SalesInvoiceID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesInvoiceItems_SalesInvoices1");
        });

        modelBuilder.Entity<SalesInvoices>(entity =>
        {
            entity.HasKey(e => e.SalesInvoiceID);

            entity.Property(e => e.CustomerPhone).HasMaxLength(30);
            entity.Property(e => e.InvoiceDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_SalesInvoices_InvoiceDate");
            entity.Property(e => e.Notes).HasMaxLength(250);

            entity.HasOne(d => d.CustomerPhoneNavigation).WithMany(p => p.SalesInvoices)
                .HasForeignKey(d => d.CustomerPhone)
                .HasConstraintName("FK_SalesInvoices_Customers1");
        });

        modelBuilder.Entity<Suppliers>(entity =>
        {
            entity.HasKey(e => e.SupplierPhone);

            entity.Property(e => e.SupplierPhone).HasMaxLength(30);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())", "DF_Suppliers_CreatedAt");
            entity.Property(e => e.SupplierName).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
