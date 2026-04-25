using System.Net.Http.Json;
using GoodHamburger.Web.Models;

namespace GoodHamburger.Web.Services;

public interface IProductApiService
{
    Task<List<ProductModel>> GetAllAsync();
}

public class ProductApiService : IProductApiService
{
    private readonly HttpClient _http;

    public ProductApiService(HttpClient http) => _http = http;

    public async Task<List<ProductModel>> GetAllAsync()
    {
        var result = await _http.GetFromJsonAsync<List<ProductModel>>("api/product");
        return result ?? new();
    }
}
