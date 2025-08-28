using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_5.Data.Conection
{
    public interface IDataHelper
    {
        DataTable ExecuteSP(string sp, Dictionary<string, object> parameters = null);
        //  int ExecuteNonQuery(string sp);
        //   object ExecuteScalar(string sp);
    }
}
