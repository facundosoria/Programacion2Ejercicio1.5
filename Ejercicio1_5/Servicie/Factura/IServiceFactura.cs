using Ejercicio1_5.Domain;

namespace Ejercicio1_5.Servicie
{
    public interface IServiceFactura
    {
        List<Factura> GetAll();
        Factura GetById(int id);
        void Add(Factura factura);
        void Update(Factura factura);
        void Delete(int id);
    }
}