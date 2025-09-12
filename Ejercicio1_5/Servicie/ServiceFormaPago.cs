using Ejercicio1_5.Domain;
using Ejercicio1_5.Data.FormaPago;

namespace Ejercicio1_5.Servicie
{
    public class ServiceFormaPago
    {
        private readonly IFormaPagoRepository _repo;
        public ServiceFormaPago(IFormaPagoRepository repo)
        {
            _repo = repo;
        }

        public List<FormaPago> GetAll() => _repo.GetAll();
        public FormaPago? GetById(int id) => _repo.GetById(id);
        public void Add(FormaPago formaPago) => _repo.Add(formaPago);
        public void Update(FormaPago formaPago) => _repo.Update(formaPago);
        public void Delete(int id) => _repo.Delete(id);
    }
}
