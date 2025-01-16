
using DocumentStorage.API.Data;
using Microsoft.EntityFrameworkCore;

namespace DocumentStorage.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string allowAllPolicy = "AllowAll";
            const string dbConnectionString = "DocumentStorageDbConnectionString";


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString(dbConnectionString);

            builder.Services.AddDbContext<DocumentStorageDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(allowAllPolicy, policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod();
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
