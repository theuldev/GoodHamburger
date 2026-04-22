using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Application.Models.ViewModels
{
    public record OrderResponse(
        Guid Id,
        string OrderNumber,
        decimal Subtotal,
        decimal Discount,
        decimal Total,
        List<OrderItemResponse> Items
    );
    public record OrderItemResponse(
         Guid ProductId,
         string ProductName,
         string ProductCode,
         int Quantity,
         decimal UnitPrice,
         decimal Total
    );
}
