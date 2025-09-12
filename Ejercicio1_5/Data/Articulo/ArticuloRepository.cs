using Ejercicio1_5.Domain;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Ejercicio1_5.Data.Articulo
{
    public class ArticuloRepository : IArticuloRepository
    {

        private SqlConnection _connection;
        private SqlTransaction _transaction;

        public ArticuloRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public List<Domain.Articulo> GetAll()
        {
            List<Domain.Articulo> articulos = new List<Domain.Articulo>();
            using (var cmd = new SqlCommand("GetAllArticulo", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        articulos.Add(new Domain.Articulo
                        {
                            IdArticulo = Convert.ToInt32(reader["IdArticulo"]),
                            Nombre = reader["Nombre"].ToString(),
                            PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"])
                        });
                    }
                }
            }
            return articulos;
        }
    

        public Domain.Articulo? GetById(int id)
        {
            Domain.Articulo? articulo = null;
            using (var cmd = new SqlCommand("GetArticuloById", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdArticulo", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        articulo = new Domain.Articulo
                        {
                            IdArticulo = Convert.ToInt32(reader["IdArticulo"]),
                            Nombre = reader["Nombre"].ToString(),
                            PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"])
                        };
                    }
                }
            }
            return articulo;
        }

        public void Add(Domain.Articulo articulo)
        {
            using (var cmd = new SqlCommand("InsertArticulo", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", articulo.Nombre);
                cmd.Parameters.AddWithValue("@PrecioUnitario", articulo.PrecioUnitario);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Domain.Articulo articulo)
        {
            using (var cmd = new SqlCommand("UpdateArticulo", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdArticulo", articulo.IdArticulo);
                cmd.Parameters.AddWithValue("@Nombre", articulo.Nombre);
                cmd.Parameters.AddWithValue("@PrecioUnitario", articulo.PrecioUnitario);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var cmd = new SqlCommand("DeleteArticulo", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdArticulo", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

      
    


      