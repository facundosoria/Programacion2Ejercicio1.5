using Ejercicio1_5.Domain;

namespace Ejercicio1_5.Servicie
{
    public interface IServiceFormaPago
    {
        List<FormaPago> GetAll();
        FormaPago GetById(int id);
        void Add(FormaPago formaPago);
        void Update(FormaPago formaPago);
        void Delete(int id);
    }
}