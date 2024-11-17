using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Extensions.Http;
using WeatherService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<WeatherService.WeatherService>()
    .AddPolicyHandler(GetRetryPolicy());

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();


static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError() // خطاهای HTTP 5xx, 408 و مشکلات شبکه
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt)); // 3 تلاش مجدد با فاصله‌های افزایشی
}