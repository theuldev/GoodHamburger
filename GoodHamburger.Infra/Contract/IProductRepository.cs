using GoodHamburger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Infra.Contract
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
    }
}
