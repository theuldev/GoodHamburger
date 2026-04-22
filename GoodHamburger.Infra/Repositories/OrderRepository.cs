using GoodHamburger.Domain.Entities;
using GoodHamburger.Infra.Context;
using GoodHamburger.Infra.Contract;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Orders.CountAsync();
        }

        public Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order); 
            return Task.CompletedTask;
        }

        public async Task<(IEnumerable<Order> Items, int Total)> GetAllAsync(int page, int pageSize, string? search, string? sort, string? order)
        {
          var query = _context.Orders.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();

                query = query.Where(o =>
                    o.OrderNumber.ToLower().Contains(s) ||
                    o.Items.Any(i => i.Product.Name.ToLower().Contains(s))
                );
            }
            if (!string.IsNullOrWhiteSpace(sort))
            {
                var ascending = string.IsNullOrWhiteSpace(order) || order.ToLowerInvariant() != "desc";
                if (sort.Equals("number", StringComparison.OrdinalIgnoreCase))
                    query = ascending ? query.OrderBy(c => c.OrderNumber) : query.OrderByDescending(c => c.OrderNumber);
                else if (sort.Equals("createdat", StringComparison.OrdinalIgnoreCase))
                    query = ascending ? query.OrderBy(c => c.CreatedAt) : query.OrderByDescending(c => c.CreatedAt);

            }

            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total); 
        }

        public Task<Order?> GetByIdAsync(Guid id)
        {
            var order = _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);  
            return order; 
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(); 
        }

        public Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            return Task.CompletedTask; 
        }
    }
}
