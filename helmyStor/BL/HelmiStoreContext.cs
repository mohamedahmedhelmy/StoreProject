using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BL
{
    public partial class HelmiStoreContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public HelmiStoreContext()
        {
        }

        public HelmiStoreContext(DbContextOptions<HelmiStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } 
        public virtual DbSet<Customer> Customers{ get; set; } 
        public virtual DbSet<Invoices> Invoices{ get; set; } 
        public virtual DbSet<Order> Orders{ get; set; }
        public virtual DbSet<Product> Products { get; set; } 
        public virtual DbSet<Slider> Sliders { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.CategoryId, "IX_products_CategoryId");

                entity.Property(e => e.BuyPrice).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}