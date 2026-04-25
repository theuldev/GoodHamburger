using GoodHamburger.Application.Interfaces;
using GoodHamburger.Application.Models.ViewModels;
using GoodHamburger.Infra.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(ProductResponse.FromEntity);
        }

        public async Task<ProductResponse?> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null ? null : ProductResponse.FromEntity(product);
        }
    }
}
