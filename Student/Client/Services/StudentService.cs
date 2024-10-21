using System.Net.Http.Json;
using System.Threading.Tasks;
using global::Student.Shared;


namespace Student.Client.Services
{
   
    public class StudentService
    {
        private readonly HttpClient _httpClient;

        public StudentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<StudentModel>> GetStudentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<StudentModel>>("api/students");
        }

        public async Task<StudentModel> AddStudentAsync(StudentModel student)
        {
            var response = await _httpClient.PostAsJsonAsync("api/students", student);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<StudentModel>();
        }

        public async Task<StudentModel> UpdateStudentAsync(int id, StudentModel student)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/students/{id}", student);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<StudentModel>();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/students/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
