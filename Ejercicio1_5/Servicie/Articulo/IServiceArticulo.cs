using Ejercicio1_5.Domain;

namespace Ejercicio1_5.Servicie
{
    public interface IServiceArticulo
    {
        List<Articulo> GetAll();
        Articulo GetById(int id);
        void Add(Articulo articulo);
        void Update(Articulo articulo);
        void Delete(int id);
    }
}