
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
			var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
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
