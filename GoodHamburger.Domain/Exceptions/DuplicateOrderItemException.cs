using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Domain.Exceptions
{
    public class DuplicateOrderItemException : Exception
    {
        public DuplicateOrderItemException(string item) : base($"Item duplicado no pedido: {item}. Cada produto só pode ser adicionado uma vez.")
        {
        }
    }
}
