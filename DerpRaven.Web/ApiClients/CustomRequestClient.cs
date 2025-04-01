using System.Net.Http.Json;
using DerpRaven.Shared.Dtos;

namespace DerpRaven.Web.ApiClients;


public class CustomRequestClient(HttpClient httpClient) : ICustomRequestClient
{
    // needs authentication
    public async Task<List<CustomRequestDto>?> GetAllCustomRequestsAsync()
    {
        var response = await httpClient.GetAsync("api/CustomRequest");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<CustomRequestDto>>();
    }

    // needs authentication
    public async Task<bool> CreateCustomRequestAsync(CustomRequestDto customRequest)
    {
        var response = await httpClient.PostAsJsonAsync("api/CustomRequest", customRequest);
        return response.IsSuccessStatusCode;
    }

    // needs authentication
    public async Task<CustomRequestDto?> GetCustomRequestByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"api/CustomRequest/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CustomRequestDto>();
    }

    // needs authentication
    public async Task<List<CustomRequestDto>?> GetCustomRequestsByUserAsync(int userId)
    {
        var response = await httpClient.GetAsync($"api/CustomRequest/user/{userId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<CustomRequestDto>>();
    }

    // needs authentication
    public async Task<List<CustomRequestDto>?> GetCustomRequestsByStatusAsync(string status)
    {
        var response = await httpClient.GetAsync($"api/CustomRequest/status/{status}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<CustomRequestDto>>();
    }

    // needs authentication
    public async Task<List<CustomRequestDto>?> GetCustomRequestsByTypeAsync(string productType)
    {
        var response = await httpClient.GetAsync($"api/CustomRequest/type/{productType}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<CustomRequestDto>>();
    }

    // needs authentication
    public async Task<bool> ChangeStatusAsync(int id, string status)
    {
        var response = await httpClient.PatchAsync($"api/CustomRequest/{id}/status", new StringContent(status));
        return response.IsSuccessStatusCode;
    }
}
