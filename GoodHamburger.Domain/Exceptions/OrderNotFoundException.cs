using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Domain.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(Guid orderId) : base($"O Pedido {orderId} não foi encontrado")
        {
        }
    }
}
