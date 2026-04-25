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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
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
            var seedDate = new DateTime(2026, 4, 22, 18, 54, 29, 760, DateTimeKind.Utc);
            modelBuilder.Entity<Product>().HasData(
                new Product(new Guid("4506afff-af71-499f-a69b-fcaee39f903b"), "X Burger", 5.00m, ProductCategory.Sandwich, seedDate),
                new Product(new Guid("94d7d6eb-11d2-41d0-95fe-f18a251a6823"), "X Egg", 4.50m, ProductCategory.Sandwich, seedDate),
                new Product(new Guid("ecd27c78-5b16-40f5-8be0-cb46007ea321"), "X Bacon", 7.00m, ProductCategory.Sandwich, seedDate),
                new Product(new Guid("f4f9560e-abc0-4efe-ac0f-555c7f2778cb"), "Batata frita", 2.00m, ProductCategory.Side, seedDate),
                new Product(new Guid("f9942189-3a73-45f4-9b1a-2c5fd76205f0"), "Refrigerante", 2.50m, ProductCategory.Drink, seedDate)
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