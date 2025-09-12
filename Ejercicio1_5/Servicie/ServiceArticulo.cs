
using Ejercicio1_5.Domain;
using Ejercicio1_5.Data.Articulo;
using Ejercicio1_5.Data;

namespace Ejercicio1_5.Servicie
{
    public class ServiceArticulo
    {
        
        private readonly string _connection;
        public ServiceArticulo(string connection)
        {
            _connection = connection;
        }

        public List<Articulo> GetAll()
        {
            List<Articulo> articulos = new List<Articulo>();
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                articulos = unitOfWork.Articulos.GetAll();
                unitOfWork.Commit();
            }
            return articulos;
        }
        public Articulo? GetById(int id)
        {
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                return unitOfWork.Articulos.GetById(id);
            }
        }
        public void Add(Articulo articulo)
        {
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                unitOfWork.Articulos.Add(articulo);
                unitOfWork.Commit();
            }
        }
        public void Update(Articulo articulo)
        {
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                unitOfWork.Articulos.Update(articulo);
                unitOfWork.Commit();
            }
        }
        public void Delete(int id)
        {
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                unitOfWork.Articulos.Delete(id);
                unitOfWork.Commit();
            }
        }
    }
}
