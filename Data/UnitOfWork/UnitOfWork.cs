using Ejercicio1_5.Data.Factura;
using Ejercicio1_5.Data.Articulo;
using Ejercicio1_5.Data.Detalles_Factura;
using Ejercicio1_5.Data.FormaPago;
using Ejercicio1_5.Data.Conection;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Ejercicio1_5.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;
        public IFacturaRepository Facturas;
        public IArticuloRepository Articulos;
        public IDetalle_FacturaRepository DetallesFactura;
        public IFormaPagoRepository FormasPago; 

        private readonly IDataHelper _dataHelper;

        public UnitOfWork(IDataHelper dataHelper)
        {
            _dataHelper = dataHelper;
            Facturas = new FacturaRepository(_dataHelper);
            Articulos = new ArticuloRepository(_dataHelper);
            DetallesFactura = new Detalle_FacturaRepository(_dataHelper);
            FormasPago = new FormaPagoRepository(_dataHelper);
        }

        public void Dispose()
        {
            // Dispose of any resources if necessary
        }
    }
}


