using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio1_5.Domain;

namespace Ejercicio1_5.Data.Detalles_Factura
{
    public interface IDetalle_FacturaRepository
    {
        List<Domain.DetalleFactura> GetAll();
        List<Domain.DetalleFactura> GetByFactura(int idFactura);
        Domain.DetalleFactura GetById(int idDetalle);
        void Add(Domain.DetalleFactura detalle);
        void Update(Domain.DetalleFactura detalle);
        void Delete(int idDetalle);

    }
}
