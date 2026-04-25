using GoodHamburger.Application.Interfaces;
using GoodHamburger.Application.Models.InputModels;
using GoodHamburger.Application.Models.ViewModels;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Infra.Contract;

namespace GoodHamburger.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository   _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository   = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<OrderResponse> CreateAsync(CreateOrderRequest request)
    {
        if (request.Items == null || !request.Items.Any())
            throw new InvalidOrderException();

        var order = Order.Create();

        foreach (var item in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product == null)
                throw new ProductNotExistException(item.ProductId);

            order.AddItem(product, item.Quantity);
        }

        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveAsync();

        return OrderResponse.FromEntity(order);
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            throw new OrderNotFoundException(id);

        await _orderRepository.DeleteAsync(order);
        await _orderRepository.SaveAsync();
    }

    public async Task<PagedResponse<OrderResponse>> GetAllAsync(
        int page, int pageSize, string? search, string? sort, string? order)
    {
        var result = await _orderRepository.GetAllAsync(page, pageSize, search, sort, order);

        return new PagedResponse<OrderResponse>
        {
            Page     = page,
            PageSize = pageSize,
            Total    = result.Total,
            Data     = result.Items.Select(OrderResponse.FromEntity).ToList()
        };
    }

    public async Task<OrderResponse?> GetByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) return null;
        return OrderResponse.FromEntity(order);
    }

    public async Task<OrderResponse> UpdateAsync(Guid id, UpdateOrderRequest request)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            throw new OrderNotFoundException(id);

        if (request.Items == null || !request.Items.Any())
            throw new InvalidOrderException();

        order.ClearItems();

        foreach (var item in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product == null)
                throw new ProductNotExistException(item.ProductId);

            order.AddItem(product, item.Quantity);
        }

        await _orderRepository.UpdateAsync(order);
        await _orderRepository.SaveAsync();

        return OrderResponse.FromEntity(order);
    }
}