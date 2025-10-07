using DirectoryService.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var dSServiceConnectionString = builder.Configuration.GetSection("DSServiceDb");

builder.Services.AddDbContext<DirectoryServiceDbContext>(options =>
{
    options.UseNpgsql(dSServiceConnectionString.Value);
});

var app = builder.Build();

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
