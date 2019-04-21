using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ArtcomCore
{
    public partial class ArtcomDBContext : DbContext
    {
        public ArtcomDBContext()
        {
        }

        public ArtcomDBContext(DbContextOptions<ArtcomDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TCustomers> TCustomers { get; set; }
        public virtual DbSet<TOrder> TOrder { get; set; }
        public virtual DbSet<TOrderProduct> TOrderProduct { get; set; }
        public virtual DbSet<TProduct> TProduct { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-ENCTF5A;Initial Catalog=ArtcomDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<TCustomers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("t_customers");

                entity.Property(e => e.CustomerName).IsRequired();
            });

            modelBuilder.Entity<TOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("t_order");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TOrder)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_order_t_customers");
            });

            modelBuilder.Entity<TOrderProduct>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.ToTable("t_order_product");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TOrderProduct)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_order_product_t_order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TOrderProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_t_order_product_t_product");
            });

            modelBuilder.Entity<TProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("t_product");

                entity.Property(e => e.ProductName).IsRequired();

                entity.Property(e => e.ProductPrice).IsRequired();
            });
        }
    }
}
