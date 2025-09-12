using System.Data;
using System.Collections.Generic;
using Ejercicio1_5.Data.Detalles_Factura;
using Ejercicio1_5.Servicie;
using Ejercicio1_5.Domain;
using Ejercicio1_5.Data.Factura;
using Ejercicio1_5.Data.Articulo;
using Ejercicio1_5.Data;
using DotNetEnv;

Env.Load();
string connectionString = Env.GetString("CONNECTION_STRING");

List<Factura> facturas;
var serviceFactura = new ServiceFactura(connectionString);
facturas = serviceFactura.GetAll();

foreach (var f in facturas)
{
    Console.WriteLine($"Nro: {f.NroFactura}, Fecha: {f.Fecha:yyyy-MM-dd}, Cliente: {f.Cliente}, Forma de Pago: {f.FormaPago.Nombre}");
    Console.WriteLine($"Detalle de factura:");
    foreach (var df in f.Detalles)
    {
        Console.WriteLine($"\tDetalle Id: {df.IdDetalle}, Articulo: {df.Articulo.Nombre}, Cantidad: {df.Cantidad}");
    }
}

var factura2 = serviceFactura.GetById(3005);
Console.WriteLine($"Nro: {factura2.NroFactura}, Fecha: {factura2.Fecha:yyyy-MM-dd}, Cliente: {factura2.Cliente}, Forma de Pago: {factura2.FormaPago.Nombre}");
Console.WriteLine($"Detalle de factura:");
foreach (var df in factura2.Detalles)
{
    Console.WriteLine($"\tDetalle Id: {df.IdDetalle}, Articulo: {df.Articulo.Nombre}, Cantidad: {df.Cantidad}");
}

serviceFactura.Add(new Factura
{
    NroFactura = 3010,
    Fecha = DateTime.Now,
    Cliente = "Cliente Nuevo",
    FormaPago = new FormaPago { IdFormaPago = 1 }, // Asumiendo que 1 es un Id válido
    Detalles = new List<DetalleFactura>
    {
        new DetalleFactura { Articulo = new Articulo { IdArticulo = 1 }, Cantidad = 2 }, // Asumiendo que 1 es un Id válido
        new DetalleFactura { Articulo = new Articulo { IdArticulo = 2 }, Cantidad = 3 }  // Asumiendo que 2 es un Id válido
    }
});

var facturaUpdate = serviceFactura.GetById(3010);
Console.WriteLine($"Nro: {facturaUpdate.NroFactura}, Fecha: {facturaUpdate.Fecha:yyyy-MM-dd}, Cliente: {facturaUpdate.Cliente}, Forma de Pago: {facturaUpdate.FormaPago.Nombre}");
Console.WriteLine($"Detalle de factura:");
foreach (var df in facturaUpdate.Detalles)
{
    Console.WriteLine($"\tDetalle Id: {df.IdDetalle}, Articulo: {df.Articulo.Nombre}, Cantidad: {df.Cantidad}");
}
