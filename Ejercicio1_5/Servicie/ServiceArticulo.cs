using Ejercicio1_5.Domain;
using Ejercicio1_5.Data.Articulo;

namespace Ejercicio1_5.Servicie
{
    public class ServiceArticulo
    {
        private readonly IArticuloRepository _repo;
        public ServiceArticulo(IArticuloRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Articulo> GetAll() => _repo.GetAll();
        public Articulo? GetById(int id) => _repo.GetById(id);
        public void Add(Articulo articulo) => _repo.Add(articulo);
        public void Update(Articulo articulo) => _repo.Update(articulo);
        public void Delete(int id) => _repo.Delete(id);
    }
}
