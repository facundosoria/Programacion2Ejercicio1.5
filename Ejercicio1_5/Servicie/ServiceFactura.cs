using Ejercicio1_5.Domain;
using Ejercicio1_5.Data.Factura;
using Ejercicio1_5.Data;

namespace Ejercicio1_5.Servicie
{
    public class ServiceFactura
    {
        private readonly string _connection;
        public ServiceFactura(string connectionString)
        {
            _connection = connectionString;
        }

        public List<Factura> GetAll()
        {
            List<Factura> facturas = new List<Factura>();
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                facturas = unitOfWork.Facturas.GetAll();
                unitOfWork.Commit();
            }
            return facturas;
        }


        public Factura? GetById(int nroFactura)
        {
            Factura? factura;
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                factura = unitOfWork.Facturas.GetById(nroFactura);
                unitOfWork.Commit();
            }
            return factura;
        }

        public void Add(Factura factura)
        {
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                unitOfWork.Facturas.Add(factura);
                unitOfWork.Commit();
            }
        }

        public void Update(Factura factura)
        {
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                unitOfWork.Facturas.Update(factura);
                unitOfWork.Commit();
            }
        }

        public void Delete(int nroFactura)
        {
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                unitOfWork.Facturas.Delete(nroFactura);
                unitOfWork.Commit();
            }
        }
    }
}
