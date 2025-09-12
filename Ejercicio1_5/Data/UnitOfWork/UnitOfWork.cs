using Ejercicio1_5.Data.Factura;
using Ejercicio1_5.Data.Articulo;
using Ejercicio1_5.Data.Detalles_Factura;
using Ejercicio1_5.Data.FormaPago;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ejercicio1_5.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;

        public IFacturaRepository Facturas { get; }
        public IArticuloRepository Articulos { get; }
        public IDetalle_FacturaRepository DetallesFactura { get; }
        public IFormaPagoRepository FormasPago { get; }

        public UnitOfWork(string _connectionString)
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();

            Facturas = new FacturaRepository(_connection, _transaction);
            Articulos = new ArticuloRepository(_connection, _transaction);
            DetallesFactura = new Detalle_FacturaRepository(_connection, _transaction);
            FormasPago = new FormaPagoRepository(_connection, _transaction);
        }

        public void Commit()
        {
            _transaction?.Commit();
            _transaction = _connection.BeginTransaction();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction = _connection.BeginTransaction();
        }
        
        public void Dispose()
        {
            _transaction?.Dispose();
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
            _connection.Dispose();
        }

 
    }
}


