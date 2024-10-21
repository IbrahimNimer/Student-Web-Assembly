using Student.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace Student.Client.Services
{
    public class EnrollmentService
    {
        private readonly HttpClient _httpClient;

        public EnrollmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EnrollmentModel>> GetEnrollmentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EnrollmentModel>>("api/enrollments");
        }

        public async Task AddEnrollmentAsync(EnrollmentModel enrollment)
        {
            var response = await _httpClient.PostAsJsonAsync("api/enrollments", enrollment);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error adding enrollment: {errorContent}");
            }
        }

        public async Task DeleteEnrollmentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/enrollments/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
