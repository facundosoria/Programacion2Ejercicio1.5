using Ejercicio1_5.Domain;
using FormaPagoEntity = Ejercicio1_5.Domain.FormaPago;
using System.Collections.Generic;

namespace Ejercicio1_5.Data.FormaPago
{
    public interface IFormaPagoRepository
    {
    List<FormaPagoEntity> GetAll();
    Domain.FormaPago GetById(int id);
    void Add(Domain.FormaPago formaPago);
    void Update(Domain.FormaPago formaPago);
    void Delete(int id);
    }
}
