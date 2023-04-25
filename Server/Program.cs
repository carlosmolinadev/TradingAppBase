using Application.Contracts.Broker;
using Application.Contracts.Persistance;
using Application.Features.Commands;
using Binance.Net.Clients;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects;
using Infrastructure.Broker;
using Infrastructure.Persistance.Repositories;
using MudBlazor.Services;
using Npgsql;
using System.Data.Common;
using Template.Infrastructure.Persistance.Dapper.Repositories;

var builder = WebApplication.CreateBuilder(args);
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var binanceApiKey = builder.Configuration.GetSection("BinanceApiKey").Value;
var binanceApiSecret = builder.Configuration.GetSection("BinanceApiSecretKey").Value;

builder.Services.AddMudServices();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//builder.Services.AddSingleton<IMarketDataProvider, MarketDataProvider>(); 
//builder.Services.AddSingleton<IBinanceClient, BinanceClient>();
//builder.Services.AddSingleton<IBinanceSocketClient, BinanceSocketClient>();

builder.Services.AddScoped<DbConnection>((sp) => new NpgsqlConnection(connectionString));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<TradeCommand>();


builder.Services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));


BinanceClient.SetDefaultOptions(new BinanceClientOptions()
{
    ApiCredentials = new BinanceApiCredentials(binanceApiKey, binanceApiSecret)
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
