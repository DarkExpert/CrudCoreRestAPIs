using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SDWrox.DataModel.Models;

public partial class SdwroxModelContext : DbContext
{
    public SdwroxModelContext()
    {
    }

    public SdwroxModelContext(string connectionString) : base(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
    {
        
    }
    public SdwroxModelContext(DbContextOptions<SdwroxModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbOrder> TbOrders { get; set; }

    public virtual DbSet<TbOrderDetail> TbOrderDetails { get; set; }

    public virtual DbSet<TbProduct> TbProducts { get; set; }

    public virtual DbSet<TbShop> TbShops { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-DENKRMG\\BMSSQLSERVER;Initial Catalog=SDWroxModel;Encrypt=False;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbOrder>(entity =>
        {
            entity.ToTable("TbOrder");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Number).HasMaxLength(50);
            entity.Property(e => e.Owner).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(500);

            entity.HasOne(d => d.Shop).WithMany(p => p.TbOrders)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbOrder_TbShop");
        });

        modelBuilder.Entity<TbOrderDetail>(entity =>
        {
            entity.ToTable("TbOrderDetail");

            entity.Property(e => e.Discount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Order).WithMany(p => p.TbOrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbOrderDetail_TbOrder");

            entity.HasOne(d => d.Product).WithMany(p => p.TbOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbOrderDetail_TbProduct");
        });

        modelBuilder.Entity<TbProduct>(entity =>
        {
            entity.ToTable("TbProduct");

            entity.Property(e => e.Title).HasMaxLength(500);
        });

        modelBuilder.Entity<TbShop>(entity =>
        {
            entity.ToTable("TbShop");

            entity.Property(e => e.Address).HasMaxLength(1000);
            entity.Property(e => e.EmailAddress).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.PhoneNumber).HasMaxLength(16);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
