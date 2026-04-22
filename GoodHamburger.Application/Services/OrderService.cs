using GoodHamburger.Application.Interfaces;
using GoodHamburger.Application.Models.InputModels;
using GoodHamburger.Application.Models.ViewModels;
using MovimentAPI.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Application.Services
{
    public class OrderService : IOrderService
    {
        public Task<OrderResponse> CreateAsync(CreateOrderRequest request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<OrderResponse>> GetAllAsync(int page, int pageSize, string? search, string? sort, string? order)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponse?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponse> UpdateAsync(Guid id, UpdateOrderRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
