using Microsoft.EntityFrameworkCore;
using Supermercado.Infrastructure.Data;
using Supermercado.Application.Interfaces;
using Supermercado.Infrastructure.Repositories;
using Supermercado.API.Middlewares;
using Microsoft.Extensions.Diagnostics.HealthChecks;

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
        
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>("database")
            .AddCheck("self", () =>
            {
                return HealthCheckResult.Healthy("API está funcionando");
            });

        var app = builder.Build();

        // Swagger middleware
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        
        app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";

                var result = new
                {
                    status = report.Status.ToString(),
                    totalDuration = report.TotalDuration.TotalMilliseconds,
                    checks = report.Entries.Select(x => new
                    {
                        name = x.Key,
                        status = x.Value.Status.ToString(),
                        duration = x.Value.Duration.TotalMilliseconds,
                        error = x.Value.Exception?.Message
                    })
                };

                await context.Response.WriteAsJsonAsync(result);
            }
        });

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}