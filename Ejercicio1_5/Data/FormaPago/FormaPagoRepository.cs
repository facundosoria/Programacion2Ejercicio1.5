using Ejercicio1_5.Domain;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ejercicio1_5.Data.FormaPago
{
    public class FormaPagoRepository : IFormaPagoRepository
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        public FormaPagoRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }
    

        public List<Domain.FormaPago> GetAll()
        {
            var formaPagos = new List<Domain.FormaPago>();
            using (var cmd = new SqlCommand("GetAllFormaPagos", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        formaPagos.Add(new Domain.FormaPago
                        {
                            IdFormaPago = Convert.ToInt32(reader["IdFormaPago"]),
                            Nombre = reader["Nombre"].ToString(),
                        });
                    }
                }
            }
            return formaPagos;
        }

   public Domain.FormaPago GetById(int id)
    {
        using (var cmd = new SqlCommand("GetFormaPagoById", _connection, _transaction))
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdFormaPago", id);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Domain.FormaPago
                    {
                        IdFormaPago = Convert.ToInt32(reader["IdFormaPago"]),
                        Nombre = reader["Nombre"].ToString(),
                    };
                }
            }
        }
        return null;
    }

    public void Add(Domain.FormaPago formaPago)
        {
            using (var cmd = new SqlCommand("AddFormaPago", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", formaPago.Nombre);
                cmd.ExecuteNonQuery();
            }
        }

    public void Update(Domain.FormaPago formaPago)
        {
            using (var cmd = new SqlCommand("UpdateFormaPago", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdFormaPago", formaPago.IdFormaPago);
                cmd.Parameters.AddWithValue("@Nombre", formaPago.Nombre);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var cmd = new SqlCommand("DeleteFormaPago", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdFormaPago", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

