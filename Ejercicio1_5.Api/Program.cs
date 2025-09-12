
using DotNetEnv;
var builder = WebApplication.CreateBuilder(args);

Env.Load("../.env");
string connectionString = Env.GetString("CONNECTION_STRING");
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<Ejercicio1_5.Servicie.ServiceArticulo>(_ => new Ejercicio1_5.Servicie.ServiceArticulo(connectionString));
builder.Services.AddScoped<Ejercicio1_5.Servicie.ServiceDetalleFactura>(_ => new Ejercicio1_5.Servicie.ServiceDetalleFactura(connectionString));
builder.Services.AddScoped<Ejercicio1_5.Servicie.ServiceFactura>(_ => new Ejercicio1_5.Servicie.ServiceFactura(connectionString));
builder.Services.AddScoped<Ejercicio1_5.Servicie.ServiceFormaPago>(_ => new Ejercicio1_5.Servicie.ServiceFormaPago(connectionString));
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


