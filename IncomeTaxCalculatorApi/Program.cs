using IncomeTaxCalculator.Domain.Repositories;
using IncomeTaxCalculator.Domain.Repositories.DbContexts;
using IncomeTaxCalculator.Domain.Repositories.Interfaces;
using IncomeTaxCalculator.Domain.Services;
using IncomeTaxCalculator.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#if DEBUG_INMEMORYREPOSITORY

builder.Services.AddSingleton<IIncomeTaxBandRepository, IncomeTaxBandInMemoryRepository>();

#else

string connectionString = builder.Configuration.GetConnectionString("IncomeTaxBandsDb");
builder.Services.AddDbContextPool<IncomeTaxBandSqlServerDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IIncomeTaxBandRepository, IncomeTaxBandSqlServerRepository>();

#endif

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
