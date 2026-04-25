using GoodHamburger.Application.Interfaces;
using GoodHamburger.Application.Models.InputModels;
using GoodHamburger.Application.Services;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Infra.Contract;
using Moq;
using Xunit;

namespace GoodHamburger.Tests.Application;

public static class TestTraits
{
    public const string Service = "Service";
    public const string Method = "Method";
    public const string Scenario = "Scenario";
}

public static class TestScenarios
{
    public const string Success = "Success";
    public const string InvalidItems = "InvalidItems";
    public const string NotFound = "NotFound";
    public const string ValidationError = "ValidationError";
    public const string Duplicate = "Duplicate";
}

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepository = new();
    private readonly Mock<IProductRepository> _productRepository = new();
    private readonly IOrderService _orderService;

    public OrderServiceTests()
    {
        _orderService = new OrderService(_orderRepository.Object, _productRepository.Object);
    }

    [Fact]
    [Trait(TestTraits.Service, "Order")]
    [Trait(TestTraits.Method, "CreateAsync")]
    [Trait(TestTraits.Scenario, TestScenarios.Success)]
    public async Task CreateAsync_ValidRequest_ReturnsOrderResponse()
    {
        var sandwich = new Product("X Burger", 5.00m, ProductCategory.Sandwich);
        var drink = new Product("Refrigerante", 2.50m, ProductCategory.Drink);

        _productRepository.Setup(r => r.GetByIdAsync(sandwich.Id)).ReturnsAsync(sandwich);
        _productRepository.Setup(r => r.GetByIdAsync(drink.Id)).ReturnsAsync(drink);
        _orderRepository.Setup(r => r.AddAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);
        _orderRepository.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        var request = new CreateOrderRequest(
        [
            new CreateOrderItemRequest(sandwich.Id, 1),
            new CreateOrderItemRequest(drink.Id, 1),
        ]);

        var result = await _orderService.CreateAsync(request);

        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal(7.50m, result.Subtotal);
        Assert.Equal(1.125m, result.Discount);
        Assert.Equal(6.375m, result.Total);
    }

    [Fact]
    [Trait(TestTraits.Service, "Order")]
    [Trait(TestTraits.Method, "CreateAsync")]
    [Trait(TestTraits.Scenario, TestScenarios.InvalidItems)]
    public async Task CreateAsync_EmptyItems_ThrowsInvalidOrderException()
    {
        var request = new CreateOrderRequest([]);

        await Assert.ThrowsAsync<InvalidOrderException>(() => _orderService.CreateAsync(request));
    }

    [Fact]
    [Trait(TestTraits.Service, "Order")]
    [Trait(TestTraits.Method, "CreateAsync")]
    [Trait(TestTraits.Scenario, TestScenarios.NotFound)]
    public async Task CreateAsync_UnknownProduct_ThrowsProductNotExistException()
    {
        var unknownId = Guid.NewGuid();
        _productRepository.Setup(r => r.GetByIdAsync(unknownId)).ReturnsAsync((Product?)null);

        var request = new CreateOrderRequest([new CreateOrderItemRequest(unknownId, 1)]);

        await Assert.ThrowsAsync<ProductNotExistException>(() => _orderService.CreateAsync(request));
    }

    [Fact]
    [Trait(TestTraits.Service, "Order")]
    [Trait(TestTraits.Method, "CreateAsync")]
    [Trait(TestTraits.Scenario, TestScenarios.Duplicate)]
    public async Task CreateAsync_DuplicateCategory_ThrowsDuplicateOrderItemException()
    {
        var sandwich1 = new Product("X Burger", 5.00m, ProductCategory.Sandwich);
        var sandwich2 = new Product("X Egg", 4.50m, ProductCategory.Sandwich);

        _productRepository.Setup(r => r.GetByIdAsync(sandwich1.Id)).ReturnsAsync(sandwich1);
        _productRepository.Setup(r => r.GetByIdAsync(sandwich2.Id)).ReturnsAsync(sandwich2);

        var request = new CreateOrderRequest(
        [
            new CreateOrderItemRequest(sandwich1.Id, 1),
            new CreateOrderItemRequest(sandwich2.Id, 1),
        ]);

        await Assert.ThrowsAsync<DuplicateOrderItemException>(() => _orderService.CreateAsync(request));
    }

    [Fact]
    [Trait(TestTraits.Service, "Order")]
    [Trait(TestTraits.Method, "DeleteAsync")]
    [Trait(TestTraits.Scenario, TestScenarios.NotFound)]
    public async Task DeleteAsync_NotFound_ThrowsOrderNotFoundException()
    {
        var id = Guid.NewGuid();
        _orderRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Order?)null);

        await Assert.ThrowsAsync<OrderNotFoundException>(() => _orderService.DeleteAsync(id));
    }

    [Fact]
    [Trait(TestTraits.Service, "Order")]
    [Trait(TestTraits.Method, "GetByIdAsync")]
    [Trait(TestTraits.Scenario, TestScenarios.Success)]
    public async Task GetByIdAsync_ExistingOrder_ReturnsOrderResponse()
    {
        var sandwich = new Product("X Burger", 5.00m, ProductCategory.Sandwich);
        var order = Order.Create();
        order.AddItem(sandwich);

        _orderRepository.Setup(r => r.GetByIdAsync(order.Id)).ReturnsAsync(order);

        var result = await _orderService.GetByIdAsync(order.Id);

        Assert.NotNull(result);
        Assert.Equal(order.Id, result.Id);
        Assert.Single(result.Items);
    }

    [Fact]
    [Trait(TestTraits.Service, "Order")]
    [Trait(TestTraits.Method, "GetByIdAsync")]
    [Trait(TestTraits.Scenario, TestScenarios.NotFound)]
    public async Task GetByIdAsync_NotFound_ReturnsNull()
    {
        var id = Guid.NewGuid();
        _orderRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Order?)null);

        var result = await _orderService.GetByIdAsync(id);

        Assert.Null(result);
    }
}