using Ejercicio1_5.Domain;
using System.Collections.Generic;

namespace Ejercicio1_5.Data.Articulo
{
    public interface IArticuloRepository
    {
        List<Domain.Articulo> GetAll();
        Domain.Articulo? GetById(int id);
        void Add(Domain.Articulo articulo);
        void Update(Domain.Articulo articulo);
        void Delete(int id);
    }
}
