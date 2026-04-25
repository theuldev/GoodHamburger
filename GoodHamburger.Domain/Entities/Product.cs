using GoodHamburger.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Domain.Entities
{
    public class Product : IEntityBase
    {
        public Product() { }

        public Product(string name, decimal price, ProductCategory category)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Category = category;
            CreatedAt = DateTime.UtcNow;
        }

        public Product(Guid id, string name, decimal price, ProductCategory category, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
            CreatedAt = createdAt;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public ProductCategory Category { get; private set; }   
        public DateTime UpdateAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}
