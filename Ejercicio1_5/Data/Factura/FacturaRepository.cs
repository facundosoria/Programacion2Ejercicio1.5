using Ejercicio1_5.Domain;
using System.Collections.Generic;
using Ejercicio1_5.Data.FormaPago;
using Ejercicio1_5.Data.Detalles_Factura;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Ejercicio1_5.Data.Factura
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public FacturaRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }


        public List<Domain.Factura> GetAll()
        {
            var facturas = new List<Domain.Factura>();

            // 1. Leer solo los datos básicos de la factura
            using (var cmd = new SqlCommand("GetAllFactura", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        facturas.Add(new Domain.Factura
                        {
                            NroFactura = Convert.ToInt32(reader["NroFactura"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Cliente = reader["Cliente"].ToString(),
                            // Guardá IdFormaPago para después
                            FormaPago = new Domain.FormaPago { IdFormaPago = Convert.ToInt32(reader["IdFormaPago"]) }
                        });
                    }
                }
            }

            // 2. Ahora, para cada factura, cargar detalles y forma de pago
            foreach (var factura in facturas)
            {
                factura.Detalles = new Detalle_FacturaRepository(_connection, _transaction)
                    .GetByFactura(factura.NroFactura);
            }

                foreach (var factura in facturas)
            {
                factura.FormaPago = new FormaPagoRepository(_connection, _transaction)
                    .GetById(factura.FormaPago.IdFormaPago);
            }
            return facturas;
        }



        public Domain.Factura GetById(int nroFactura)
        {
            Domain.Factura factura = null;
            using (var cmd = new SqlCommand("GetFacturaById", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NroFactura", nroFactura);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        factura = new Domain.Factura
                        {
                            NroFactura = Convert.ToInt32(reader["NroFactura"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            FormaPago = new Domain.FormaPago { IdFormaPago = Convert.ToInt32(reader["IdFormaPago"]) },
                            Cliente = reader["Cliente"].ToString()
                        };
                    }
                }
                if (factura != null)
                {
                    factura.Detalles = new Detalle_FacturaRepository(_connection, _transaction)
                        .GetByFactura(factura.NroFactura);
                    factura.FormaPago = new FormaPagoRepository(_connection, _transaction)
                        .GetById(factura.FormaPago.IdFormaPago);
                }
                else
                {
                    throw new KeyNotFoundException($"Factura con NroFactura {nroFactura} no encontrada.");
                }
            }
            return factura;
           
        }

        public void Add(Domain.Factura factura)
        {
            // Insertar la factura principal y obtener el NroFactura generado
            using (var cmd = new SqlCommand("InsertFactura", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fecha", factura.Fecha);
                cmd.Parameters.AddWithValue("@IdFormaPago", factura.FormaPago.IdFormaPago);
                cmd.Parameters.AddWithValue("@Cliente", factura.Cliente);

                // Ejecutar y obtener el NroFactura generado
                var nroFactura = (int)cmd.ExecuteScalar();
                factura.NroFactura = nroFactura;
            }

            // Insertar los detalles
            if (factura.Detalles != null)
            {
                var detalleRepo = new Detalle_FacturaRepository(_connection, _transaction);
                foreach (var detalle in factura.Detalles)
                {
                    detalle.NroFactura = factura.NroFactura;
                    detalleRepo.Add(detalle);
                }
            }
        }

        public void Update(Domain.Factura factura)
        {
            
            using (var cmd = new SqlCommand("UpdateFactura", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NroFactura", factura.NroFactura);
                cmd.Parameters.AddWithValue("@Fecha", factura.Fecha);
                cmd.Parameters.AddWithValue("@IdFormaPago", factura.FormaPago.IdFormaPago);
                cmd.Parameters.AddWithValue("@Cliente", factura.Cliente);
                cmd.ExecuteNonQuery();
            }

            var detalleRepo = new Detalle_FacturaRepository(_connection, _transaction);
            var detallesExistentes = detalleRepo.GetByFactura(factura.NroFactura);
            foreach (var detalle in detallesExistentes)
            {
                detalleRepo.Delete(detalle.IdDetalle);
            }


            if (factura.Detalles != null)
            {
                foreach (var detalle in factura.Detalles)
                {
                    detalle.NroFactura = factura.NroFactura;
                    detalleRepo.Add(detalle);
                }
            }
        }

        public void Delete(int nroFactura)
        {
            using (var cmd = new SqlCommand("DeleteFactura", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NroFactura", nroFactura);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
