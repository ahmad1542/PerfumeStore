using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Infrastructure.Persistence;

public partial class PerfumeStoreDbContext : DbContext {
    public PerfumeStoreDbContext() {
    }

    public PerfumeStoreDbContext(DbContextOptions<PerfumeStoreDbContext> options) : base(options) {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Inventory> Inventory { get; set; }

    public virtual DbSet<MoneyAccount> MoneyAccounts { get; set; }

    public virtual DbSet<MoneyTransfer> MoneyTransfers { get; set; }

    public virtual DbSet<PaymentVoucher> PaymentVouchers { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }

    public virtual DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }

    public virtual DbSet<ReceiptVoucher> ReceiptVouchers { get; set; }

    public virtual DbSet<SalesInvoiceItem> SalesInvoiceItems { get; set; }

    public virtual DbSet<SalesInvoice> SalesInvoices { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Brand>(entity => {
            entity.HasKey(e => e.ID);

            entity.HasIndex(e => e.Name, "IX_Brands").IsUnique();

            entity.Property(e => e.BrandDescription)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<Customer>(entity => {
            entity.HasKey(e => e.Phone);

            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_Customers_CreatedAt");
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<Expense>(entity => {
            entity.HasKey(e => e.ID);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Date)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_Expenses_ExpenseDate");
            entity.Property(e => e.Type).HasMaxLength(150);
            entity.Property(e => e.Notes).HasMaxLength(250);

            entity.HasOne(d => d.MoneyAccount).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.MoneyAccountID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Expenses_MoneyAccounts1");
        });

        modelBuilder.Entity<Inventory>(entity => {
            entity.HasKey(e => e.ProductID);

            entity.Property(e => e.ProductID).ValueGeneratedNever();

            entity.HasOne(d => d.Product).WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(d => d.ProductID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_Products1");
        });

        modelBuilder.Entity<MoneyAccount>(entity => {
            entity.HasKey(e => e.ID);

            entity.Property(e => e.AccountName).HasMaxLength(50);
            entity.Property(e => e.CurrentBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Notes).HasMaxLength(250);
        });

        modelBuilder.Entity<MoneyTransfer>(entity => {
            entity.HasKey(e => e.ID);

            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.TransferAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Date)
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

        modelBuilder.Entity<PaymentVoucher>(entity => {
            entity.HasKey(e => e.ID);

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.Date)
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

            entity.HasOne(d => d.Supplier).WithMany(p => p.PaymentVouchers)
                .HasForeignKey(d => d.SupplierPhone)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentVouchers_Suppliers1");
        });

        modelBuilder.Entity<ProductCategory>(entity => {
            entity.HasKey(e => e.ID);

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(250);
        });

        modelBuilder.Entity<Product>(entity => {
            entity.HasKey(e => e.ID);

            entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt).HasPrecision(0);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandID)
                .HasConstraintName("FK_Products_Brands1");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_ProductCategories1");
        });

        modelBuilder.Entity<PurchaseInvoiceItem>(entity => {
            entity.HasKey(e => e.ID);

            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseInvoiceItems)
                .HasForeignKey(d => d.ProductID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseInvoiceItems_Products1");

            entity.HasOne(d => d.PurchaseInvoice).WithMany(p => p.PurchaseInvoiceItems)
                .HasForeignKey(d => d.PurchaseInvoiceID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseInvoiceItems_PurchaseInvoices1");
        });

        modelBuilder.Entity<PurchaseInvoice>(entity => {
            entity.HasKey(e => e.ID);

            entity.Property(e => e.Date)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_PurchaseInvoices_InvoiceDate");
            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.SupplierPhone).HasMaxLength(30);

            entity.HasOne(d => d.Supplier).WithMany(p => p.PurchaseInvoices)
                .HasForeignKey(d => d.SupplierPhone)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseInvoices_Suppliers1");
        });

        modelBuilder.Entity<ReceiptVoucher>(entity => {
            entity.HasKey(e => e.ID);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CustomerPhone).HasMaxLength(30);
            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.Date)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_ReceiptVouchers_ReceiptVoucherDate");

            entity.HasOne(d => d.Customer).WithMany(p => p.ReceiptVouchers)
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

        modelBuilder.Entity<SalesInvoiceItem>(entity => {
            entity.HasKey(e => e.ID);

            entity.HasOne(d => d.Product).WithMany(p => p.SalesInvoiceItems)
                .HasForeignKey(d => d.ProductID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesInvoiceItems_Products1");

            entity.HasOne(d => d.SalesInvoice).WithMany(p => p.SalesInvoiceItems)
                .HasForeignKey(d => d.SalesInvoiceID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesInvoiceItems_SalesInvoices1");
        });

        modelBuilder.Entity<SalesInvoice>(entity => {
            entity.HasKey(e => e.ID);

            entity.Property(e => e.CustomerPhone).HasMaxLength(30);
            entity.Property(e => e.Date)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_SalesInvoices_InvoiceDate");
            entity.Property(e => e.Notes).HasMaxLength(250);

            entity.HasOne(d => d.Customer).WithMany(p => p.SalesInvoices)
                .HasForeignKey(d => d.CustomerPhone)
                .HasConstraintName("FK_SalesInvoices_Customers1");
        });

        modelBuilder.Entity<Supplier>(entity => {
            entity.HasKey(e => e.Phone);

            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())", "DF_Suppliers_CreatedAt");
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
