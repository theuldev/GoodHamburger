using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Infra.Context
{
    public class ApplicationDbContext : DbContext
    {
        private const string connectionString = "";

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            if (!optionsBuilder.IsConfigured)
            {
                try
                {
                    optionsBuilder.UseNpgsql(connectionString);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(o =>
            {
                o.HasKey(o => o.Id);
                o.Property(o => o.OrderNumber)
                 .IsRequired()
                 .HasMaxLength(30);
                o.HasIndex(o => o.OrderNumber)
                 .IsUnique();
                o.HasMany(o => o.Items)
                 .WithOne(i => i.Order)
                 .HasForeignKey(i => i.OrderId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Product>(builder =>
            {
                builder.HasKey(p => p.Id);

                builder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(p => p.Category)
                    .IsRequired();
            });
            modelBuilder.Entity<Product>().HasData(
                new Product("X Burger", 5.00m, ProductCategory.Sandwich),
                new Product("X Egg", 4.50m, ProductCategory.Sandwich),
                new Product("X Bacon", 7.00m, ProductCategory.Sandwich),
                new Product("Batata frita", 2.00m, ProductCategory.Side),
                new Product("Refrigerante", 2.50m, ProductCategory.Drink)
            );

            modelBuilder.Entity<OrderItem>(builder =>
            {
                builder.HasKey(i => i.Id);

                builder.Property(i => i.Quantity)
                    .IsRequired();

                builder.HasOne(i => i.Product)
                    .WithMany()
                    .HasForeignKey(i => i.ProductId);
            });
        }
    } 
}