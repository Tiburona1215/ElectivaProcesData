using ElectivaProcesData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectivaProcesData.DB
{
    public class DBsalesContext : DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrdersDetails> OrderDetail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=.;Database=AnalisisDeVentas;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrdersDetails>()
                .HasKey(od => new { od.OrderID, od.ProductID });

            builder.Entity<OrdersDetails>()
                .HasOne<Orders>()
                .WithMany()
                .HasForeignKey(od => od.OrderID);

            builder.Entity<OrdersDetails>()
                .HasOne<Products>()
                .WithMany()
                .HasForeignKey(od => od.ProductID);

            builder.Entity<Orders>()
                .HasOne<Customers>()
                .WithMany()
                .HasForeignKey(o => o.CustomerID);
        }
    }
}
