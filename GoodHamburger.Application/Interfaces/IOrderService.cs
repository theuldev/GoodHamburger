using GoodHamburger.Application.Models.InputModels;
using GoodHamburger.Application.Models.ViewModels;
using MovimentAPI.Application.ViewModels;

namespace GoodHamburger.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateAsync(CreateOrderRequest request);
        Task<OrderResponse?> GetByIdAsync(Guid id);
        Task<PagedResponse<OrderResponse>> GetAllAsync(
            int page,
            int pageSize,
            string? search,
            string? sort,
            string? order);

        Task<OrderResponse> UpdateAsync(Guid id, UpdateOrderRequest request);
        Task DeleteAsync(Guid id);
    }
}