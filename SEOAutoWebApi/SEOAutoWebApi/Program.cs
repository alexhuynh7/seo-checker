using SEOAutoWebApi.Cache;
using SEOAutoWebApi.Features;
using SEOAutoWebApi.Infrastructure.Interface;
using SEOAutoWebApi.Infrastructure.Services;
using SEOAutoWebApi.Middlewares;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<GoogleSearchService>();
builder.Services.AddHttpClient<BingSearchService>();

// DI
builder.Services.AddSingleton<ICacheService, MemoryCacheService>();
builder.Services.AddSingleton<ISearchServiceFactory, SearchServiceFactory>();
builder.Services.AddSingleton<ISearchService, GoogleSearchService>();
builder.Services.AddSingleton<ISearchService, BingSearchService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseCors("AllowAllOrigins");

app.Run();
