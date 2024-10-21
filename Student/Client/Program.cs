using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Student.Client.Services;
using System.Net.Http;

namespace Student.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Set HttpClient to use the backend API URL explicitly (replace with the correct backend port if needed)
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5080/")  // Use the backend's address
            });

            // Register StudentService for DI
            builder.Services.AddScoped<StudentService>();
            builder.Services.AddScoped<CourseService>();
            builder.Services.AddScoped<EnrollmentService>();


            await builder.Build().RunAsync();
        }
    }
}
