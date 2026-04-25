using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Domain.Exceptions
{
    public class InvalidOrderException : Exception
    {
        public InvalidOrderException() : base("O pedido deve conter ao menos um item.")
        {
        }
    }
}
