
using Ejercicio1_5.Domain;
using Ejercicio1_5.Data.Detalles_Factura;
using Ejercicio1_5.Data;

namespace Ejercicio1_5.Servicie
{
    public class ServiceDetalleFactura
    {
        private readonly string _connection;
        public ServiceDetalleFactura(string connection)
        {
            _connection = connection;
        }

        public List<DetalleFactura> GetAll()
        {
            List<DetalleFactura> detalles = new List<DetalleFactura>();
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                detalles = unitOfWork.DetallesFactura.GetAll();
                unitOfWork.Commit();
            }       
            return detalles;
        }
        public DetalleFactura? GetById(int id)
        {
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                return unitOfWork.DetallesFactura.GetById(id);
            }
        }
        public void Add(DetalleFactura detalle)
        {
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                unitOfWork.DetallesFactura.Add(detalle);
                unitOfWork.Commit();
            }
        }
        public void Update(DetalleFactura detalle)
        {
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                unitOfWork.DetallesFactura.Update(detalle);
                unitOfWork.Commit();
            }
        }
        public void Delete(int id)
        {
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                unitOfWork.DetallesFactura.Delete(id);
                unitOfWork.Commit();
            }
        }
    }
}
