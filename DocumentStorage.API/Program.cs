
using DocumentStorage.API.Configurations;
using DocumentStorage.API.Contracts;
using DocumentStorage.API.Data;
using DocumentStorage.API.Repository;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

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

            builder.Services.AddControllers()
                .AddXmlDataContractSerializerFormatters();
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

            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IDocumentsRepository, DocumentsRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options
                        .WithTitle("Document Storage API")
                        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
                });
            }

            app.UseHttpsRedirection();

            app.UseCors(allowAllPolicy);

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
