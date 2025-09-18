
using Ejercicio1_5.Domain;
using Ejercicio1_5.Data;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio1_5.Servicie
{
    public class ServiceFactura : IServiceFactura
    {
        private readonly AppDbContext _context;

        public ServiceFactura(AppDbContext context)
        {
            _context = context;
        }

        public List<Factura> GetAll()
        {
            return _context.Facturas
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(d => d.IdArticuloNavigation)
                .Include(f => f.IdFormaPagoNavigation)
                .ToList();
        }

        public Factura? GetById(int id)
        {
            return _context.Facturas
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(d => d.IdArticuloNavigation)
                .Include(f => f.IdFormaPagoNavigation)
                .FirstOrDefault(f => f.NroFactura == id);
        }

        public void Add(Factura factura)
        {
            _context.Facturas.Add(factura);
            _context.SaveChanges();
        }

        public void Update(Factura factura)
        {
            _context.Facturas.Update(factura);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var factura = _context.Facturas.Find(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
                _context.SaveChanges();
            }
        }
    }
}