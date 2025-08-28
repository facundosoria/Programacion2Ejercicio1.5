using Ejercicio1_5.Domain;
using Ejercicio1_5.Data.Factura;

namespace Ejercicio1_5.Servicie
{
    public class ServiceFactura
    {
        private readonly IFacturaRepository _repo;
        public ServiceFactura(IFacturaRepository repo)
        {
            _repo = repo;
        }

        public List<Factura> GetAll() => _repo.GetAll();
        public Factura? GetById(int nroFactura) => _repo.GetById(nroFactura);
        public void Add(Factura factura) => _repo.Add(factura);
        public void Update(Factura factura) => _repo.Update(factura);
        public void Delete(int nroFactura) => _repo.Delete(nroFactura);
    }
}
