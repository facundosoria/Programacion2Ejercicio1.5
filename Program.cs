// See https://aka.ms/new-console-template for more information
using Ejercicio1_5.Data.Conection;
using System.Data;
using System.Collections.Generic;
using Ejercicio1_5.Data.Detalles_Factura;
using Ejercicio1_5.Servicie;
using Ejercicio1_5.Domain;
using Ejercicio1_5.Data.Factura;
using Ejercicio1_5.Data.Articulo;



DataHelper dataHelper = DataHelper.GetIntance();
FacturaRepository facturaRepository = new FacturaRepository(dataHelper);
ServiceFactura serviceFactura = new ServiceFactura(facturaRepository);


bool salir = false;
while (!salir)
{
    Console.WriteLine("\n--- Menú CRUD Facturas ---");
    Console.WriteLine("1. Listar facturas");
    Console.WriteLine("2. Buscar factura por Nro");
    Console.WriteLine("3. Crear factura");
    Console.WriteLine("4. Modificar factura");
    Console.WriteLine("5. Eliminar factura");
    Console.WriteLine("0. Salir");
    Console.Write("Seleccione una opción: ");
    string? opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":

            foreach (var f in serviceFactura.GetAll())
            {
                Console.WriteLine($"Nro: {f.NroFactura}, Fecha: {f.Fecha:yyyy-MM-dd}, Cliente: {f.Cliente}, Forma de Pago: {f.FormaPago.Nombre}");
                Console.WriteLine($"Detalle de factura:");
                foreach (var df in f.Detalles)
                {
                    Console.WriteLine($"\tDetalle Id: {df.IdDetalle}, Articulo: {df.Articulo.Nombre}, Cantidad: {df.Cantidad}");
                }
            }
            break;
        case "2":
            Console.Write("Ingrese Nro de factura: ");
            if (int.TryParse(Console.ReadLine(), out int nroBuscar))
            {
                var f = serviceFactura.GetById(nroBuscar);
                if (f != null) {
                    Console.WriteLine($"Nro: {f.NroFactura}, Fecha: {f.Fecha:yyyy-MM-dd}, Cliente: {f.Cliente}");
                    Console.WriteLine($"Detalle de factura:");
                    foreach (var df in f.Detalles)
                    {
                        Console.WriteLine($"\tDetalle Id: {df.IdDetalle}, Articulo: {df.Articulo.Nombre}, Cantidad: {df.Cantidad}");
                    }
                }
                    
                    
                else
                    Console.WriteLine("Factura no encontrada.");
            }
            break;
        case "3":
            Console.Write("Fecha (yyyy-MM-dd): ");
            DateTime fecha = DateTime.Parse(Console.ReadLine() ?? "");
            Console.Write("IdFormaPago: ");
            int idFormaPago = int.Parse(Console.ReadLine() ?? "");
            Console.Write("Cliente: ");
            string cliente = Console.ReadLine() ?? "";
            var nueva = new Factura {
                Fecha = fecha,
                FormaPago = new FormaPago { IdFormaPago = idFormaPago },
                Cliente = cliente,
                Detalles = new List<DetalleFactura>()
            };

            // Submenú para agregar artículos al detalle
            var dataHelperDetalle = DataHelper.GetIntance();
            ArticuloRepository  articuloRepo = new ArticuloRepository(dataHelperDetalle);
            ServiceArticulo serviceArticulo = new ServiceArticulo(articuloRepo);
            bool agregarMas = true;
            while (agregarMas)
            {
                Console.WriteLine("\n--- Agregar artículo al detalle ---");
                Console.WriteLine("Lista de artículos disponibles:");
                foreach (var art in serviceArticulo.GetAll())
                {
                    Console.WriteLine($"Id: {art.IdArticulo}, Nombre: {art.Nombre}");
                }
                Console.Write("Ingrese Id de artículo: ");
                int idArticulo = int.Parse(Console.ReadLine() ?? "");
                var articulo = serviceArticulo.GetById(idArticulo);
                if (articulo == null)
                {
                    Console.WriteLine("Artículo no encontrado.");
                }
                else
                {
                    Console.Write("Cantidad: ");
                    int cantidad = int.Parse(Console.ReadLine() ?? "");
                    nueva.Detalles.Add(new DetalleFactura {
                        Articulo = articulo,
                        Cantidad = cantidad
                    });
                }
                Console.Write("¿Agregar otro artículo? (s/n): ");
                agregarMas = Console.ReadLine()?.Trim().ToLower() == "s";
            }
            serviceFactura.Add(nueva);
            break;
        case "4":
            Console.Write("Nro de factura a modificar: ");
            if (int.TryParse(Console.ReadLine(), out int nroMod))
            {
                var f = serviceFactura.GetById(nroMod);
                if (f != null)
                {
                    Console.Write("Nueva fecha (yyyy-MM-dd): ");
                    f.Fecha = DateTime.Parse(Console.ReadLine() ?? "");
                    Console.Write("Nuevo IdFormaPago: ");
                    f.FormaPago.IdFormaPago = int.Parse(Console.ReadLine() ?? "");
                    Console.Write("Nuevo cliente: ");
                    f.Cliente = Console.ReadLine() ?? "";
                    serviceFactura.Update(f);
                    Console.WriteLine("Factura actualizada.");
                }
                else
                    Console.WriteLine("Factura no encontrada.");
            }
            break;
        case "5":
            Console.Write("Nro de factura a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int nroDel))
            {
                serviceFactura.Delete(nroDel);
                Console.WriteLine("Factura eliminada.");
            }
            break;
        case "0":
            salir = true;
            break;
        default:
            Console.WriteLine("Opción inválida.");
            break;
    }
}