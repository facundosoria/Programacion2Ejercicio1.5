using Ejercicio1_5.Domain;
using Ejercicio1_5.Data.Detalles_Factura;

namespace Ejercicio1_5.Servicie
{
    public class ServiceDetalleFactura
    {
        private readonly IDetalle_FacturaRepository _repo;
        public ServiceDetalleFactura(IDetalle_FacturaRepository repo)
        {
            _repo = repo;
        }

        public List<DetalleFactura> GetAll() => _repo.GetAll();
        public DetalleFactura? GetById(int id) => _repo.Get(id);
        public void Add(DetalleFactura detalle) => _repo.Add(detalle);
        public void Update(DetalleFactura detalle) => _repo.Update(detalle);
        public void Delete(int id) => _repo.Delete(id);
    }
}
