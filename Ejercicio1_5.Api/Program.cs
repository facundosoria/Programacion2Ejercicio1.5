using DotNetEnv;
using Ejercicio1_5.Servicie;
using Ejercicio1_5.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

Env.Load("../.env");
string connectionString = Env.GetString("CONNECTION_STRING");
// Add services to the container.
builder.Services.AddControllers();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IServiceArticulo, ServiceArticulo>();
builder.Services.AddScoped<IServiceDetalleFactura, ServiceDetalleFactura>();
builder.Services.AddScoped<IServiceFactura, ServiceFactura>();
builder.Services.AddScoped<IServiceFormaPago, ServiceFormaPago>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();  

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


