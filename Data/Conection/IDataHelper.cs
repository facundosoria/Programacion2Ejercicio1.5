using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio1_5.Domain;

namespace Ejercicio1_5.Data.Conection
{
    public interface IDataHelper
    {
        DataTable ExecuteSP(string sp, List<Parameters> parameters = null);
    }
}
