using GoodHamburger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodHamburger.Application.Models.ViewModels
{
    public record OrderResponse
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;

        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        public List<OrderItemResponse> Items { get; set; } = new();

        public static OrderResponse FromEntity(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                Subtotal = order.Subtotal,
                Discount = order.Discount,
                Total = order.Total,
                Items = order.Items.Select(i => new OrderItemResponse(
                    i.ProductId,
                    i.Product.Name,
                    i.Quantity,
                    i.UnitPrice,
                    i.GetTotal()
                )).ToList()
            };
        }
    }
}