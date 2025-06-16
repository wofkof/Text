using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Repositories;
using System.Data;
using ProductApi.Services;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// CORS 設定
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// 註冊 Controllers
builder.Services.AddControllers();

// 註冊自訂 Repository（Dapper）
builder.Services.AddScoped<ProductRepository>();

// 註冊 Services
builder.Services.AddScoped<IProductService, ProductService>();

// EF Core 用來建表（Migrations）
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=ProductApi.db"));

// Dapper 使用的連線（手動注入）
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqliteConnection("Data Source=ProductApi.db"));



// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

// 測試 GET Endpoint（可留可刪）
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        )).ToArray();

    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
