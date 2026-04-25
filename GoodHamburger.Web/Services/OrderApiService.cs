using System.Net.Http.Json;
using GoodHamburger.Web.Models;

namespace GoodHamburger.Web.Services;

public interface IOrderApiService
{
    Task<PagedResponse<OrderModel>> GetAllAsync(int page = 1, int pageSize = 10);
    Task<OrderModel?> GetByIdAsync(Guid id);
    Task<(OrderModel? Order, string? Error)> CreateAsync(CreateOrderRequest request);
    Task<(OrderModel? Order, string? Error)> UpdateAsync(Guid id, CreateOrderRequest request);
    Task<string?> DeleteAsync(Guid id);
}

public class OrderApiService : IOrderApiService
{
    private readonly HttpClient _http;

    public OrderApiService(HttpClient http) => _http = http;

    public async Task<PagedResponse<OrderModel>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        var result = await _http.GetFromJsonAsync<PagedResponse<OrderModel>>(
            $"api/order?page={page}&pageSize={pageSize}");
        return result ?? new();
    }

    public async Task<OrderModel?> GetByIdAsync(Guid id)
    {
        return await _http.GetFromJsonAsync<OrderModel>($"api/order/{id}");
    }

    public async Task<(OrderModel? Order, string? Error)> CreateAsync(CreateOrderRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/order", request);
        if (response.IsSuccessStatusCode)
        {
            var order = await response.Content.ReadFromJsonAsync<OrderModel>();
            return (order, null);
        }
        var err = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        return (null, err?.Error ?? "Erro ao criar pedido.");
    }

    public async Task<(OrderModel? Order, string? Error)> UpdateAsync(Guid id, CreateOrderRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/order/{id}", request);
        if (response.IsSuccessStatusCode)
        {
            var order = await response.Content.ReadFromJsonAsync<OrderModel>();
            return (order, null);
        }
        var err = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        return (null, err?.Error ?? "Erro ao atualizar pedido.");
    }

    public async Task<string?> DeleteAsync(Guid id)
    {
        var response = await _http.DeleteAsync($"api/order/{id}");
        if (response.IsSuccessStatusCode) return null;
        var err = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        return err?.Error ?? "Erro ao remover pedido.";
    }
}

file class ErrorResponse
{
    public string? Error { get; set; }
}
