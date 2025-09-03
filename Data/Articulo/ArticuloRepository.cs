using Ejercicio1_5.Domain;
using System.Collections.Generic;
using Ejercicio1_5.Data.Conection;

namespace Ejercicio1_5.Data.Articulo
{
    public class ArticuloRepository : IArticuloRepository
    {
         private IDataHelper _dataHelper;

         public ArticuloRepository(IDataHelper dataHelper)
         {
             _dataHelper = dataHelper;
         }

        public List<Domain.Articulo> GetAll()
        {
            List<Domain.Articulo> articulos = new List<Domain.Articulo>();
            var dt = _dataHelper.ExecuteSP("GetAllArticulo");
            foreach (System.Data.DataRow row in dt.Rows)
            {
                articulos.Add(new Domain.Articulo
                {
                    IdArticulo = Convert.ToInt32(row["IdArticulo"]),
                    Nombre = row["Nombre"].ToString()
                });
            }
            return articulos;
        }

        public Domain.Articulo? GetById(int id)
        {
            Domain.Articulo? articulo = null;
            var parameters = new List<Parameters>
            {
                new Parameters { Name = "@IdArticulo", Value = id }
            };
            var dt = _dataHelper.ExecuteSP("GetArticuloById", parameters);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                articulo = new Domain.Articulo
                {
                    IdArticulo = Convert.ToInt32(row["IdArticulo"]),
                    Nombre = row["Nombre"].ToString()
                };
            }
            return articulo;
        }

        public void Add(Domain.Articulo articulo)
        {
            var parameters = new List<Parameters>
            {
                new Parameters { Name = "@Nombre", Value = articulo.Nombre }
            };
            _dataHelper.ExecuteSP("InsertArticulo", parameters);
        }

        public void Update(Domain.Articulo articulo)
        {
            var parameters = new List<Parameters>
            {
                new Parameters { Name = "@IdArticulo", Value = articulo.IdArticulo },
                new Parameters { Name = "@Nombre", Value = articulo.Nombre }
            };
            _dataHelper.ExecuteSP("UpdateArticulo", parameters);
        }

        public void Delete(int id)
        {
            var parameters = new List<Parameters>
            {
                new Parameters { Name = "@IdArticulo", Value = id }
            };
            _dataHelper.ExecuteSP("DeleteArticulo", parameters);
        }
    }
}
