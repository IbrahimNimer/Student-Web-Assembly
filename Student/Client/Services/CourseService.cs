using Student.Shared;
using System.Net.Http.Json;

namespace Student.Client.Services
{
    public class CourseService
    {
        private readonly HttpClient _httpClient;

        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CourseModel>> GetCoursesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CourseModel>>("api/courses");
        }

        public async Task<CourseModel> AddCourseAsync(CourseModel course)
        {
            var response = await _httpClient.PostAsJsonAsync("api/courses", course);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CourseModel>();
        }

        public async Task<CourseModel> UpdateCourseAsync(int id, CourseModel course)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/courses/{id}", course);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CourseModel>();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/courses/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
