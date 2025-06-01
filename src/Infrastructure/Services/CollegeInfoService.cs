using Application.Common.Models;
using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public interface ICollegeInfoServiceClient
    {
        Task<CollegeInfoDto> GetIntendedMajorAsync(Guid majorId);
    }
    public class CollegeInfoService : ICollegeInfoServiceClient
    {
        private readonly HttpClient _httpClient;
        public CollegeInfoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<CollegeInfoDto> GetIntendedMajorAsync(Guid majorId)
        {
            var response = await _httpClient.GetAsync($"collegeinfo/major/{majorId}");

            if (response.IsSuccessStatusCode)
            {
                var majorName = await response.Content.ReadFromJsonAsync<CollegeInfoDto>();
                return majorName ?? new();
            }

            return null;
        }
    }
}
