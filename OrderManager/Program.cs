using Serilog;
using Microsoft.FeatureManagement;
using OrderManager.Application.Interfaces;
using OrderManager.Application.Services;
using OrderManager.Application.Common;
using OrderManager.Domain.Interfaces;
using OrderManager.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddFeatureManagement();
builder.Services.AddSingleton<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<ITaxProvider, TaxProvider>();
builder.Services.AddScoped<IPedidoAppService, PedidoAppService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
public partial class Program { }