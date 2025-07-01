using IncomeTaxCalculator.Api.Repositories;
using IncomeTaxCalculator.Api.Repositories.DbContexts;
using IncomeTaxCalculator.Api.Repositories.Interfaces;
using IncomeTaxCalculator.Api.Services;
using IncomeTaxCalculator.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("IncomeTaxBandsDb");
builder.Services.AddDbContextPool<IncomeTaxBandSqlServerDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IIncomeTaxBandRepository, IncomeTaxBandRepositorySqlServer>();
builder.Services.AddScoped<IIncomeTaxCalculatorService, UnitedKingdomIncomeTaxCalculatorService>();

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

app.Run();
