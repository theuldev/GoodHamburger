
using GoodHamburger.Application.Models.ViewModels;

namespace GoodHamburger.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllAsync();
        Task<ProductResponse?> GetByIdAsync(Guid id);
    }
}