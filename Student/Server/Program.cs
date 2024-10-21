using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Student.Server.Repository;

namespace Student
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // Set up connection string
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

            // Register your custom StudentContext as a scoped service.
            builder.Services.AddScoped<StudentContext>(provider => new StudentContext(connectionString));
            builder.Services.AddScoped<CourseContext>(provider => new CourseContext(connectionString));
            builder.Services.AddScoped<EnrollmentContext>(provider => new EnrollmentContext(connectionString));


            // Enable CORS to allow frontend communication.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            // Register HttpClient (without base address for server-side)
            builder.Services.AddScoped(sp => new HttpClient());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();  // Use HSTS for production.
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            // Apply the CORS policy.
            app.UseCors("AllowAll");

            // Map endpoints.
            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}