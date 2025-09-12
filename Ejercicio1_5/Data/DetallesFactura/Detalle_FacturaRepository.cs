using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio1_5.Data.Articulo;
using Microsoft.Data.SqlClient;
using System.Data;
using Ejercicio1_5.Domain;



namespace Ejercicio1_5.Data.Detalles_Factura
{
    public class Detalle_FacturaRepository : IDetalle_FacturaRepository
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        


        public Detalle_FacturaRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }
    

        public List<DetalleFactura> GetAll()
        {
            List<DetalleFactura> detalles = new List<DetalleFactura>();
            using (var cmd = new SqlCommand("GetAllDetalleFactura", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        detalles.Add(new DetalleFactura
                        {
                            IdDetalle = Convert.ToInt32(reader["IdDetalle"]),
                            NroFactura = Convert.ToInt32(reader["NroFactura"]),
                            Articulo = new Domain.Articulo { IdArticulo = Convert.ToInt32(reader["IdArticulo"]) },
                            Cantidad = Convert.ToInt32(reader["Cantidad"])
                        });
                    }
                }

                foreach (var detalle in detalles)
                {
                    detalle.Articulo = new ArticuloRepository(_connection, _transaction).GetById(detalle.Articulo.IdArticulo);
                }
            }
            return detalles;
        }

        public DetalleFactura Get(int idDetalle)
        {
            DetalleFactura detalle = null;
            using (var cmd = new SqlCommand("GetDetalleFacturaById", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdDetalle", idDetalle);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        detalle = new Domain.DetalleFactura
                        {
                            IdDetalle = Convert.ToInt32(reader["IdDetalle"]),
                            NroFactura = Convert.ToInt32(reader["NroFactura"]),
                            Articulo = new Domain.Articulo { IdArticulo = Convert.ToInt32(reader["IdArticulo"]) },
                            Cantidad = Convert.ToInt32(reader["Cantidad"])
                        };
                    }
                }
                if (detalle == null)
                {
                    throw new KeyNotFoundException($"No se encontró el detalle con Id {idDetalle}.");
                }
                else
                {
                    detalle.Articulo = new ArticuloRepository(_connection, _transaction)
                    .GetById(detalle.Articulo.IdArticulo);
                }
            }
            return detalle;
        }

        public List<Domain.DetalleFactura> GetByFactura(int nroFactura)
        {
            List<Domain.DetalleFactura> detalles = new List<Domain.DetalleFactura>();
            using (var cmd = new SqlCommand("GetDetalleFacturaByFactura", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NroFactura", nroFactura);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        detalles.Add(new Domain.DetalleFactura
                        {
                            IdDetalle = Convert.ToInt32(reader["IdDetalle"]),
                            NroFactura = Convert.ToInt32(reader["NroFactura"]),
                            Articulo = new Domain.Articulo { IdArticulo = Convert.ToInt32(reader["IdArticulo"]) },
                            Cantidad = Convert.ToInt32(reader["Cantidad"])
                        });
                    }
                }
            }
            var articuloRepo = new ArticuloRepository(_connection, _transaction);
            foreach (var detalle in detalles)
            {
                detalle.Articulo = articuloRepo.GetById(detalle.Articulo.IdArticulo);
            }
            return detalles;
        }

        public void Add(Domain.DetalleFactura detalle)
        {
            using (var cmd = new SqlCommand("InsertDetalleFactura", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NroFactura", detalle.NroFactura);
                cmd.Parameters.AddWithValue("@IdArticulo", detalle.Articulo?.IdArticulo);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.ExecuteNonQuery();
            }
        }




        public void Update(Domain.DetalleFactura detalle)
        {
            using (var cmd = new SqlCommand("UpdateDetalleFactura", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdDetalle", detalle.IdDetalle);
                cmd.Parameters.AddWithValue("@IdArticulo", detalle.Articulo?.IdArticulo);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int idDetalle)
        {
            using (var cmd = new SqlCommand("DeleteDetalleFactura", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdDetalle", idDetalle);
                cmd.ExecuteNonQuery();
            }
        }

    }

}
    
    

