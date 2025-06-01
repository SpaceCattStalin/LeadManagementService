using Application.Common.Models;
using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public interface IUserServiceClient
    {
        Task<UserDto> GetUserByIdAsync(Guid userId);
    }
    public class UserServiceClient : IUserServiceClient
    {
        private readonly HttpClient _httpClient;
        public UserServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UserDto> GetUserByIdAsync(Guid userId)
        {
            var response = await _httpClient.GetAsync($"users/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<UserDto>();
                return user ?? new();
            }
            return null;
        }
    }
}
