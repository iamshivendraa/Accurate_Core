using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Accurate_Core.Models;

namespace Accurate_Core.App_Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ExcelSample> ExcelData { get; set; } = null!;
        public virtual DbSet<OrderSummary> OrderSummaries { get; set; } = null!;
        public virtual DbSet<RegisteredUsers> RegisteredUsers { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExcelDatum>(entity =>
            {
                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.StockNum).HasColumnName("stockNum");

                entity.Property(e => e.TaxCode).HasColumnName("taxCode");
            });

            modelBuilder.Entity<OrderSummary>(entity =>
            {
                entity.ToTable("OrderSummary");

                entity.Property(e => e.Vin).HasColumnName("VIN");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
