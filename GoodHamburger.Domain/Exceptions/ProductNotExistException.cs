using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Domain.Exceptions
{
    public class ProductNotExistException : Exception
    {
        public ProductNotExistException(Guid productId) : base($"O Produto {productId} não foi encontrado")
        {
            
        }
    }
}
