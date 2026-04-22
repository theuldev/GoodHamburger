using GoodHamburger.Domain.Entities;
using GoodHamburger.Infra.Context;
using GoodHamburger.Infra.Contract;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}