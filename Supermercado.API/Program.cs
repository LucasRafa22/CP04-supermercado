using Microsoft.EntityFrameworkCore;
using Supermercado.Infrastructure.Data;
using Supermercado.Application.Interfaces;
using Supermercado.Infrastructure.Repositories;
using Supermercado.API.Middlewares;

namespace Supermercado.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Controllers
        builder.Services.AddControllers();

        // Swagger (IMPORTANTE CP3)
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // DbContext Oracle
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("RecommendaContextOracle");
            options.UseOracle(connectionString);
        });

        // DI GENÉRICO (CP3 CORE)
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        var app = builder.Build();

        // Swagger middleware
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.UseMiddleware<ExceptionHandlerMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}