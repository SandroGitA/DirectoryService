using DirectoryService.Application.Locations;
using DirectoryService.Contracts;
using DirectoryService.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Exceptions;

namespace DirectoryService.API.Extensions;

public static class DiExtensions
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilogConfiguration(configuration);
        services.AddControllers();
        services.AddSwaggerGen();
        
        var dsServiceConnectionString = configuration.GetSection("DSServiceDb");

        services.AddDbContext<DirectoryServiceDbContext>(options =>
        {
            options.UseNpgsql(dsServiceConnectionString.Value);
        });

        services.AddScoped<IValidator<CreateLocationDto>, CreateLocationValidator>();
        services.AddScoped<ILocationsService, LocationsService>();
        services.AddScoped<ILocationsRepository, LocationsRepository>();
        
        return services;
    }

    private static IServiceCollection AddSerilogConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog((sp, lc) => lc
            .ReadFrom.Configuration(configuration)
            .ReadFrom.Services(sp)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("ServiceName", "DirectoryService"));

        return services;
    }
}