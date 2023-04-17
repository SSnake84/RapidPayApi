using Microsoft.AspNetCore.Authentication;
using RapidPayApi.Data;
using RapidPayApi.Handlers;
using RapidPayApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddSingleton<IUniversalFeesExchangeService, UniversalFeesExchangeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICreditCardsRepo, CreditCardsRepo>();
builder.Services.AddScoped<ICreditCardService, CreditCardService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
