using GoodHamburger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Infra.Contract
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order?> GetByIdAsync(Guid id);
        Task<(IEnumerable<Order> Items, int Total)> GetAllAsync(int page, int pageSize, string? search, string? sort, string? order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
        Task<int> CountAsync();
        Task SaveAsync();
    }
}
