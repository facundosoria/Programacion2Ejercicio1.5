using Ejercicio1_5.Domain;
using FacturaEntity = Ejercicio1_5.Domain.Factura;
using System.Collections.Generic;

namespace Ejercicio1_5.Data.Factura
{
    public interface IFacturaRepository
    {
        List<Domain.Factura> GetAll();
        Domain.Factura GetById(int nroFactura);
        void Add(Domain.Factura factura);
        void Update(Domain.Factura factura);
        void Delete(int nroFactura);
    }
}
