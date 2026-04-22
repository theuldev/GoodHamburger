using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }

        public Guid OrderId { get; private set; }
        public Order Order { get; private set; } = null!;

        public Guid ProductId { get; private set; }
        public Product Product { get; private set; } = null!;

        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        protected OrderItem() { }

        public OrderItem(Product product, int quantity)
        {
            Id = Guid.NewGuid();
            ProductId = product.Id;
            Product = product;
            Quantity = quantity;
            UnitPrice = product.Price;
        }

        public decimal GetTotal() => Quantity * UnitPrice;
    }
}
