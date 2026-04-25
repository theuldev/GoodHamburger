using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Application.Models.ViewModels
{
    public record OrderItemResponse(
           Guid ProductId,
           string ProductName,
           int Quantity,
           decimal UnitPrice,
           decimal Total
      );
}
