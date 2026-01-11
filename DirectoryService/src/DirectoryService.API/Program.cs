using DirectoryService.API.Middlewares;
using DirectoryService.Application.Locations;
using DirectoryService.Contracts;
using DirectoryService.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var dsServiceConnectionString = builder.Configuration.GetSection("DSServiceDb");

builder.Services.AddDbContext<DirectoryServiceDbContext>(options =>
{
    options.UseNpgsql(dsServiceConnectionString.Value);
});

builder.Services.AddScoped<IValidator<CreateLocationDto>, CreateLocationValidator>();
builder.Services.AddScoped<ILocationsService, LocationsService>();
builder.Services.AddScoped<ILocationsRepository, LocationsRepository>();

var app = builder.Build();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
