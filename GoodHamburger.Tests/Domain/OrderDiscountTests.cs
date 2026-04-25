using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;
using Xunit;

namespace GoodHamburger.Tests.Domain;

public static class TestTraits
{
    public const string Domain = "Domain";
    public const string Entity = "Entity";
    public const string Method = "Method";
    public const string Scenario = "Scenario";
}

public static class TestScenarios
{
    public const string Success = "Success";
    public const string NoDiscount = "NoDiscount";
    public const string Discount20 = "Discount20";
    public const string Discount15 = "Discount15";
    public const string Discount10 = "Discount10";
    public const string Duplicate = "Duplicate";
    public const string Reset = "Reset";
    public const string Calculation = "Calculation";
    public const string Generation = "Generation";
}

public class OrderDiscountTests
{
    private static Product MakeSandwich(string name = "X Burger", decimal price = 5.00m) =>
        new(name, price, ProductCategory.Sandwich);

    private static Product MakeSide(decimal price = 2.00m) =>
        new("Batata frita", price, ProductCategory.Side);

    private static Product MakeDrink(decimal price = 2.50m) =>
        new("Refrigerante", price, ProductCategory.Drink);

    [Fact]
    [Trait(TestTraits.Domain, "Order")]
    [Trait(TestTraits.Entity, "Order")]
    [Trait(TestTraits.Method, "AddItem")]
    [Trait(TestTraits.Scenario, TestScenarios.Discount20)]
    public void AddItems_SandwichSideDrink_Applies20PercentDiscount()
    {
        var order = Order.Create();
        order.AddItem(MakeSandwich());
        order.AddItem(MakeSide());
        order.AddItem(MakeDrink());

        var expectedSubtotal = 9.50m;
        var expectedDiscount = expectedSubtotal * 0.20m;
        var expectedTotal = expectedSubtotal - expectedDiscount;

        Assert.Equal(expectedSubtotal, order.Subtotal);
        Assert.Equal(expectedDiscount, order.Discount);
        Assert.Equal(expectedTotal, order.Total);
    }

    [Fact]
    [Trait(TestTraits.Domain, "Order")]
    [Trait(TestTraits.Entity, "Order")]
    [Trait(TestTraits.Method, "AddItem")]
    [Trait(TestTraits.Scenario, TestScenarios.Discount15)]
    public void AddItems_SandwichDrink_Applies15PercentDiscount()
    {
        var order = Order.Create();
        order.AddItem(MakeSandwich());
        order.AddItem(MakeDrink());

        var expectedSubtotal = 7.50m;
        var expectedDiscount = expectedSubtotal * 0.15m;
        var expectedTotal = expectedSubtotal - expectedDiscount;

        Assert.Equal(expectedSubtotal, order.Subtotal);
        Assert.Equal(expectedDiscount, order.Discount);
        Assert.Equal(expectedTotal, order.Total);
    }

    [Fact]
    [Trait(TestTraits.Domain, "Order")]
    [Trait(TestTraits.Entity, "Order")]
    [Trait(TestTraits.Method, "AddItem")]
    [Trait(TestTraits.Scenario, TestScenarios.Discount10)]
    public void AddItems_SandwichSide_Applies10PercentDiscount()
    {
        var order = Order.Create();
        order.AddItem(MakeSandwich());
        order.AddItem(MakeSide());

        var expectedSubtotal = 7.00m;
        var expectedDiscount = expectedSubtotal * 0.10m;
        var expectedTotal = expectedSubtotal - expectedDiscount;

        Assert.Equal(expectedSubtotal, order.Subtotal);
        Assert.Equal(expectedDiscount, order.Discount);
        Assert.Equal(expectedTotal, order.Total);
    }

    [Fact]
    [Trait(TestTraits.Domain, "Order")]
    [Trait(TestTraits.Entity, "Order")]
    [Trait(TestTraits.Method, "AddItem")]
    [Trait(TestTraits.Scenario, TestScenarios.NoDiscount)]
    public void AddItems_SandwichOnly_NoDiscount()
    {
        var order = Order.Create();
        order.AddItem(MakeSandwich());

        Assert.Equal(0m, order.Discount);
        Assert.Equal(order.Subtotal, order.Total);
    }

    [Fact]
    [Trait(TestTraits.Domain, "Order")]
    [Trait(TestTraits.Entity, "Order")]
    [Trait(TestTraits.Method, "AddItem")]
    [Trait(TestTraits.Scenario, TestScenarios.NoDiscount)]
    public void AddItems_SideDrinkWithoutSandwich_NoDiscount()
    {
        var order = Order.Create();
        order.AddItem(MakeSide());
        order.AddItem(MakeDrink());

        Assert.Equal(0m, order.Discount);
        Assert.Equal(order.Subtotal, order.Total);
    }

    [Fact]
    [Trait(TestTraits.Domain, "Order")]
    [Trait(TestTraits.Entity, "Order")]
    [Trait(TestTraits.Method, "AddItem")]
    [Trait(TestTraits.Scenario, TestScenarios.Duplicate)]
    public void AddItem_DuplicateSandwich_ThrowsDuplicateOrderItemException()
    {
        var order = Order.Create();
        order.AddItem(MakeSandwich("X Burger", 5.00m));

        var ex = Assert.Throws<DuplicateOrderItemException>(
            () => order.AddItem(MakeSandwich("X Egg", 4.50m)));

        Assert.Contains("X Egg", ex.Message);
    }

    [Fact]
    [Trait(TestTraits.Domain, "Order")]
    [Trait(TestTraits.Entity, "Order")]
    [Trait(TestTraits.Method, "AddItem")]
    [Trait(TestTraits.Scenario, TestScenarios.Duplicate)]
    public void AddItem_DuplicateSide_ThrowsDuplicateOrderItemException()
    {
        var order = Order.Create();
        order.AddItem(MakeSide());

        Assert.Throws<DuplicateOrderItemException>(() => order.AddItem(MakeSide()));
    }

    [Fact]
    [Trait(TestTraits.Domain, "Order")]
    [Trait(TestTraits.Entity, "Order")]
    [Trait(TestTraits.Method, "AddItem")]
    [Trait(TestTraits.Scenario, TestScenarios.Duplicate)]
    public void AddItem_DuplicateDrink_ThrowsDuplicateOrderItemException()
    {
        var order = Order.Create();
        order.AddItem(MakeDrink());

        Assert.Throws<DuplicateOrderItemException>(() => order.AddItem(MakeDrink()));
    }

    [Fact]
    [Trait(TestTraits.Domain, "Order")]
    [Trait(TestTraits.Entity, "Order")]
    [Trait(TestTraits.Method, "ClearItems")]
    [Trait(TestTraits.Scenario, TestScenarios.Reset)]
    public void ClearItems_ResetsAllTotals()
    {
        var order = Order.Create();
        order.AddItem(MakeSandwich());
        order.AddItem(MakeSide());
        order.AddItem(MakeDrink());

        order.ClearItems();

        Assert.Equal(0m, order.Subtotal);
        Assert.Equal(0m, order.Discount);
        Assert.Equal(0m, order.Total);
        Assert.Empty(order.Items);
    }

    [Fact]
    [Trait(TestTraits.Domain, "OrderItem")]
    [Trait(TestTraits.Entity, "OrderItem")]
    [Trait(TestTraits.Method, "GetTotal")]
    [Trait(TestTraits.Scenario, TestScenarios.Calculation)]
    public void OrderItem_GetTotal_ReturnsQuantityTimesUnitPrice()
    {
        var product = MakeSandwich(price: 7.00m);
        var item = new OrderItem(product, quantity: 3);

        Assert.Equal(21.00m, item.GetTotal());
    }

    [Fact]
    [Trait(TestTraits.Domain, "Order")]
    [Trait(TestTraits.Entity, "Order")]
    [Trait(TestTraits.Method, "AddItem")]
    [Trait(TestTraits.Scenario, TestScenarios.Calculation)]
    public void AddItem_WithQuantity_SubtotalCalculatedCorrectly()
    {
        var order = Order.Create();
        order.AddItem(MakeSandwich(price: 7.00m), quantity: 2);
        order.AddItem(MakeSide(price: 2.00m), quantity: 3);

        Assert.Equal(20.00m, order.Subtotal);
    }

    [Fact]
    [Trait(TestTraits.Domain, "Order")]
    [Trait(TestTraits.Entity, "Order")]
    [Trait(TestTraits.Method, "Create")]
    [Trait(TestTraits.Scenario, TestScenarios.Generation)]
    public void Create_GeneratesNonEmptyOrderNumber()
    {
        var order = Order.Create();

        Assert.NotEmpty(order.OrderNumber);
        Assert.StartsWith("ORD-", order.OrderNumber);
    }
}