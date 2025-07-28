
using Microsoft.EntityFrameworkCore;
using NokhardiaAPI.Context;
using NokhardiaAPI.DTO_s;
using NokhardiaAPI.Services;

namespace NokhardiaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var credentialsBase64 = Environment.GetEnvironmentVariable("GCS_CREDENTIALS_BASE64");
			if (!string.IsNullOrEmpty(credentialsBase64))
			{
				var jsonBytes = Convert.FromBase64String(credentialsBase64);
				var credentialsPath = Path.Combine(Directory.GetCurrentDirectory(), "Credentials");
				Directory.CreateDirectory(credentialsPath);
				var fullPath = Path.Combine(credentialsPath, "nokhardiaKey.json");
				File.WriteAllBytes(fullPath, jsonBytes);
			}
			builder.Services.AddControllers();
			builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			var gcsConfig = builder.Configuration.GetSection("GoogleCloudStorage").Get<GCSOptions>();
			builder.Services.AddSingleton(gcsConfig);
			builder.Services.AddSingleton<IGCSService, GCSService>();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
			var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
			app.Urls.Add($"http://*:{port}");

			app.MapGet("/", () => "API işləyir!");
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
