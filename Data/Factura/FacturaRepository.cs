using Ejercicio1_5.Domain;
using System.Collections.Generic;
using Ejercicio1_5.Data.Conection;
using Ejercicio1_5.Data.FormaPago;
using Ejercicio1_5.Data.Detalles_Factura;

namespace Ejercicio1_5.Data.Factura
{
    public class FacturaRepository : IFacturaRepository
    {
         private IDataHelper _dataHelper;

        public FacturaRepository(IDataHelper dataHelper)
        {
            _dataHelper = dataHelper;
        }

        public List<Domain.Factura> GetAll()
        {
            var facturas = new List<Domain.Factura>();
            var dt = _dataHelper.ExecuteSP("GetAllFactura");
            foreach (System.Data.DataRow row in dt.Rows)
            {
                facturas.Add(new Domain.Factura
                {
                    NroFactura = Convert.ToInt32(row["NroFactura"]),
                    Fecha = Convert.ToDateTime(row["Fecha"]),
                    Detalles = new Detalle_FacturaRepository(_dataHelper).GetByFactura(Convert.ToInt32(row["NroFactura"])),
                    FormaPago = new FormaPagoRepository(_dataHelper).GetById(Convert.ToInt32(row["IdFormaPago"])),
                    Cliente = row["Cliente"].ToString()
                });
            }
            return facturas;
        }

    public Domain.Factura GetById(int nroFactura)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@NroFactura", nroFactura }
            };
            var dt = _dataHelper.ExecuteSP("GetFacturaById", parameters);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                return new Domain.Factura
                {
                    NroFactura = Convert.ToInt32(row["NroFactura"]),
                    Fecha = Convert.ToDateTime(row["Fecha"]),
                    Detalles = new Detalle_FacturaRepository(_dataHelper).GetByFactura(Convert.ToInt32(row["NroFactura"])),
                    FormaPago = new FormaPagoRepository(_dataHelper).GetById(Convert.ToInt32(row["IdFormaPago"])),
                    Cliente = row["Cliente"].ToString()
                };
            }
            throw new KeyNotFoundException($"Factura con NroFactura {nroFactura} no encontrada.");
        }

    public void Add(Domain.Factura factura)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Fecha", factura.Fecha },
                { "@IdFormaPago", factura.FormaPago?.IdFormaPago },
                { "@Cliente", factura.Cliente }
            };
            _dataHelper.ExecuteSP("InsertFactura", parameters);
        }

    public void Update(Domain.Factura factura)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@NroFactura", factura.NroFactura },
                { "@Fecha", factura.Fecha },
                { "@IdFormaPago", factura.FormaPago.IdFormaPago },
                { "@Cliente", factura.Cliente }
            };
            _dataHelper.ExecuteSP("UpdateFactura", parameters);
        }

        public void Delete(int nroFactura)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@NroFactura", nroFactura }
            };
            _dataHelper.ExecuteSP("DeleteFactura", parameters);
        }
    }
}
