using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Domain.Entities;

public class Order : IEntityBase
{
    private Order()
    {
        OrderNumber = string.Empty;
        Items = new List<OrderItem>();
    }

    public static Order Create()
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmssfff}",
            CreatedAt = DateTime.UtcNow,
            Items = new List<OrderItem>()
        };
    }

    public Guid Id { get; private set; }
    public string OrderNumber { get; private set; }
    public List<OrderItem> Items { get; private set; }
    public decimal Subtotal { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Total { get; private set; }
    public DateTime UpdateAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public void AddItem(Product product, int quantity = 1)
    {
        ValidateDuplicate(product);
        var item = new OrderItem(product, quantity);
        Items.Add(item);
        Recalculate();
    }

    private void ValidateDuplicate(Product product)
    {
        var alreadyExists = Items.Any(i => i.Product.Category == product.Category);
        if (alreadyExists)
            throw new DuplicateOrderItemException(product.Name);
    }

    private void Recalculate()
    {
        Subtotal = Items.Sum(i => i.GetTotal());
        Discount = CalculateDiscount();
        Total = Subtotal - Discount;
        UpdateAt = DateTime.UtcNow;
    }

    private decimal CalculateDiscount()
    {
        var hasSandwich = Items.Any(i => i.Product.Category == ProductCategory.Sandwich);
        var hasSide     = Items.Any(i => i.Product.Category == ProductCategory.Side);
        var hasDrink    = Items.Any(i => i.Product.Category == ProductCategory.Drink);

        if (hasSandwich && hasSide && hasDrink) return Subtotal * 0.20m;
        if (hasSandwich && hasDrink)            return Subtotal * 0.15m;
        if (hasSandwich && hasSide)             return Subtotal * 0.10m;

        return 0;
    }

    public void ClearItems()
    {
        Items.Clear();
        Recalculate();
    }
}
