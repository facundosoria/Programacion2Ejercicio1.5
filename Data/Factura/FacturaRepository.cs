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
            var df = new Detalle_FacturaRepository(_dataHelper);
            var fp = new FormaPagoRepository(_dataHelper);
            foreach (System.Data.DataRow row in dt.Rows)
            {
                facturas.Add(new Domain.Factura
                {
                    NroFactura = Convert.ToInt32(row["NroFactura"]),
                    Fecha = Convert.ToDateTime(row["Fecha"]),
                    Detalles = df.GetByFactura(Convert.ToInt32(row["NroFactura"])),
                    FormaPago = fp.GetById(Convert.ToInt32(row["IdFormaPago"])),
                    Cliente = row["Cliente"].ToString()
                });
            }
            return facturas;
        }

    public Domain.Factura GetById(int nroFactura)
        {
            Domain.Factura? factura = null;
            var parameters = new List<Parameters>
            {
                new Parameters { Name = "@NroFactura", Value = nroFactura }
            };
            var dt = _dataHelper.ExecuteSP("GetFacturaById", parameters);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                factura =new Domain.Factura
                {
                    NroFactura = Convert.ToInt32(row["NroFactura"]),
                    Fecha = Convert.ToDateTime(row["Fecha"]),
                    Detalles = new Detalle_FacturaRepository(_dataHelper).GetByFactura(Convert.ToInt32(row["NroFactura"])),
                    FormaPago = new FormaPagoRepository(_dataHelper).GetById(Convert.ToInt32(row["IdFormaPago"])),
                    Cliente = row["Cliente"].ToString()
                };
            }
            return factura;
        }

public void Add(Domain.Factura factura)
{
    var parameters = new List<Parameters>
    {
        new Parameters { Name = "@Fecha", Value = factura.Fecha },
        new Parameters { Name = "@IdFormaPago", Value = factura.FormaPago?.IdFormaPago },
        new Parameters { Name = "@Cliente", Value = factura.Cliente }
    };

    var dt = _dataHelper.ExecuteSP("InsertFactura", parameters);

    int idFactura = Convert.ToInt32(dt.Rows[0]["NroFactura"]);
    factura.NroFactura = idFactura;

    if (factura.Detalles != null && factura.Detalles.Count > 0)
    {
        var detRepo = new Detalle_FacturaRepository(_dataHelper);
        foreach (var det in factura.Detalles)
        {
            det.NroFactura = idFactura;
            detRepo.Add(det);
        }
    }
}

    public void Update(Domain.Factura factura)
        {
            var parameters = new List<Parameters>
            {
                new Parameters { Name = "@NroFactura", Value = factura.NroFactura },
                new Parameters { Name = "@Fecha", Value = factura.Fecha },
                new Parameters { Name = "@IdFormaPago", Value = factura.FormaPago.IdFormaPago },
                new Parameters { Name = "@Cliente", Value = factura.Cliente }
            };
            _dataHelper.ExecuteSP("UpdateFactura", parameters);
        }


        public void Delete(int nroFactura)
        {
            var parameters = new List<Parameters>
            {
                new Parameters { Name = "@NroFactura", Value = nroFactura }
            };
            _dataHelper.ExecuteSP("DeleteFactura", parameters);
        }
    }
}
