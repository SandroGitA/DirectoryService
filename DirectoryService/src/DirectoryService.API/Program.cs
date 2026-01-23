using DirectoryService.API.Extensions;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting up");
    
    var builder = WebApplication.CreateBuilder(args);
    
    //string environment = builder.Environment.EnvironmentName;

    builder.Services.AddConfigurations(builder.Configuration);

    var app = builder.Build();

    app.Configure();
    app.Run();
}
catch
{
    Log.Fatal("Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
