using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Application.Models.InputModels
{
    public record CreateOrderItemRequest(
      Guid ProductId,
      int Quantity
  );
}
