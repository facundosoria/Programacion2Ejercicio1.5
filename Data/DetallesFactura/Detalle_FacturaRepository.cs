using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio1_5.Data.Conection;
using Ejercicio1_5.Data.Articulo;

namespace Ejercicio1_5.Data.Detalles_Factura
{
    public class Detalle_FacturaRepository : IDetalle_FacturaRepository
    {
        private IDataHelper _dataHelper;
        //Esto es una inyección de dependencias chicos
        public Detalle_FacturaRepository(IDataHelper dataHelper)
        {
            _dataHelper = dataHelper;
        }

        public List<Domain.DetalleFactura> GetAll()
        {
            List<Domain.DetalleFactura> detalles = new List<Domain.DetalleFactura>();
            var dt = _dataHelper.ExecuteSP("GetAllDetalleFactura");
            foreach (System.Data.DataRow row in dt.Rows)
            {
                detalles.Add(new Domain.DetalleFactura
                {
                    IdDetalle = Convert.ToInt32(row["IdDetalle"]),
                    NroFactura = Convert.ToInt32(row["NroFactura"]),
                    Articulo = new ArticuloRepository(_dataHelper).GetById(Convert.ToInt32(row["IdArticulo"])),
                    Cantidad = Convert.ToInt32(row["Cantidad"])
                });
            }
            return detalles;
        }
        public Domain.DetalleFactura Get(int idDetalle)
        {
            Domain.DetalleFactura detalle = null;
            var parameters = new Dictionary<string, object>
            {
                { "@IdDetalle", idDetalle }
            };
            var dt = _dataHelper.ExecuteSP("GetDetalleFacturaById", parameters);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                detalle = new Domain.DetalleFactura
                {
                    IdDetalle = Convert.ToInt32(row["IdDetalle"]),
                    NroFactura = Convert.ToInt32(row["NroFactura"]),
                    Articulo = new ArticuloRepository(_dataHelper).GetById(Convert.ToInt32(row["IdArticulo"])),
                    Cantidad = Convert.ToInt32(row["Cantidad"])
                };
            }
            return detalle;
        }

        public List<Domain.DetalleFactura> GetByFactura(int nroFactura)
        {
            List<Domain.DetalleFactura> detalles = new List<Domain.DetalleFactura>();
            var parameters = new Dictionary<string, object>
            {
                { "@NroFactura", nroFactura }
            };
            var dt = _dataHelper.ExecuteSP("GetDetalleFacturaByFactura", parameters);
            foreach (System.Data.DataRow row in dt.Rows)
            {
                detalles.Add(new Domain.DetalleFactura
                {
                    IdDetalle = Convert.ToInt32(row["IdDetalle"]),
                    NroFactura = Convert.ToInt32(row["NroFactura"]),
                    Articulo = new ArticuloRepository(_dataHelper).GetById(Convert.ToInt32(row["IdArticulo"])),
                    Cantidad = Convert.ToInt32(row["Cantidad"])
                });
            }
            return detalles;
        }

        public void Add(Domain.DetalleFactura detalle)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@NroFactura", detalle.NroFactura },
                { "@IdArticulo", detalle.Articulo?.IdArticulo },
                { "@Cantidad", detalle.Cantidad }
            };
            _dataHelper.ExecuteSP("InsertDetalleFactura", parameters);
        }

        public void Update(Domain.DetalleFactura detalle)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@IdDetalle", detalle.IdDetalle },
                { "@IdArticulo", detalle.Articulo?.IdArticulo },
                { "@Cantidad", detalle.Cantidad }
            };
            _dataHelper.ExecuteSP("UpdateDetalleFactura", parameters);
        }

        public void Delete(int idDetalle)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@IdDetalle", idDetalle }
            };
            _dataHelper.ExecuteSP("DeleteDetalleFactura", parameters);
        }

    }

}
