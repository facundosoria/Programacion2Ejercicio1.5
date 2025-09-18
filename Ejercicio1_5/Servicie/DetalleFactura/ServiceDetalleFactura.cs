using Ejercicio1_5.Data;
using Ejercicio1_5.Domain;


namespace Ejercicio1_5.Servicie
{
    public class ServiceDetalleFactura : IServiceDetalleFactura
    {
        private readonly AppDbContext _context;

        public ServiceDetalleFactura(AppDbContext context)
        {
            _context = context;
        }

        public List<DetalleFactura> GetAll()
        {
            return _context.DetalleFacturas.ToList();
        }

        public DetalleFactura? GetById(int id)
        {
            return _context.DetalleFacturas.Find(id);
        }

        public void Add(DetalleFactura detalle)
        {
            _context.DetalleFacturas.Add(detalle);
            _context.SaveChanges();
        }

        public void Update(DetalleFactura detalle)
        {
            _context.DetalleFacturas.Update(detalle);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var detalle = _context.DetalleFacturas.Find(id);
            if (detalle != null)
            {
                _context.DetalleFacturas.Remove(detalle);
                _context.SaveChanges();
            }
        }
    }
}