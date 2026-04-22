using GoodHamburger.Application.Interfaces;
using GoodHamburger.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Application.Services
{
    public class ProductService : IProductService
    {
        public Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponse?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
