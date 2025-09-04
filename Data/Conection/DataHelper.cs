using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio1_5.Domain;
using System.Reflection.Metadata;

namespace Ejercicio1_5.Data.Conection
{
    public class DataHelper : IDataHelper
    {
        private static DataHelper _instance;

    private string _stringconnection = "Data Source=localhost,1433;Initial Catalog=FacturacionDB;User ID=sa;Password=Admin123;TrustServerCertificate=True";

        private DataHelper()        
        {

        }
        public static DataHelper GetIntance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        public DataTable ExecuteSP(string sp, List<Parameters> parameter = null)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection _connection = new SqlConnection(_stringconnection))
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand(sp, _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameter != null)
                    {
                        foreach (Parameters p in parameter)
                        {
                            cmd.Parameters.AddWithValue(p.Name, p.Value);
                        }
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
            }

            return dataTable;
        }
    }
}
