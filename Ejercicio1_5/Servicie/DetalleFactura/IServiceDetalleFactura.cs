using Ejercicio1_5.Domain;

namespace Ejercicio1_5.Servicie
{
    public interface IServiceDetalleFactura
    {
        List<DetalleFactura> GetAll();
        DetalleFactura GetById(int id);
        void Add(DetalleFactura detalleFactura);
        void Update(DetalleFactura detalleFactura);
        void Delete(int id);
    }
}