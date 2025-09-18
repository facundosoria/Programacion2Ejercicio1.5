using Ejercicio1_5.Domain;
using Ejercicio1_5.Data;

namespace Ejercicio1_5.Servicie
{
    public class ServiceArticulo : IServiceArticulo
    {
        private readonly AppDbContext _context;

        public ServiceArticulo(AppDbContext context)
        {
            _context = context;
        }

        public List<Articulo> GetAll()
        {
            return _context.Articulos.ToList();
        }

        public Articulo? GetById(int id)
        {
            return _context.Articulos.Find(id);
        }

        public void Add(Articulo articulo)
        {
            _context.Articulos.Add(articulo);
            _context.SaveChanges();
        }

        public void Update(Articulo articulo)
        {
            _context.Articulos.Update(articulo);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var articulo = _context.Articulos.Find(id);
            if (articulo != null)
            {
                _context.Articulos.Remove(articulo);
                _context.SaveChanges();
            }
        }
    }
}