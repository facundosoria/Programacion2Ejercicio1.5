using Ejercicio1_5.Domain;
using Ejercicio1_5.Data.FormaPago;
using Ejercicio1_5.Data;

namespace Ejercicio1_5.Servicie
{
    public class ServiceFormaPago
    {
        private readonly string _connection;
        public ServiceFormaPago(string connection)
        {
            _connection = connection;
        }
        private readonly IFormaPagoRepository _repo;
        public ServiceFormaPago(IFormaPagoRepository repo)
        {
            _repo = repo;
        }

        public List<FormaPago> GetAll()
        {
            List<FormaPago> formasPago = new List<FormaPago>();
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                formasPago = unitOfWork.FormasPago.GetAll();
                unitOfWork.Commit();
            }
            return formasPago;
        }
        public FormaPago? GetById(int id)
        {
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                return unitOfWork.FormasPago.GetById(id);
            }
        }
        public void Add(FormaPago formaPago)
        {
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                unitOfWork.FormasPago.Add(formaPago);
                unitOfWork.Commit();
            }
        }
        public void Update(FormaPago formaPago)
        {
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                unitOfWork.FormasPago.Update(formaPago);
                unitOfWork.Commit();
            }
        }
        public void Delete(int id)
        {
            using (var unitOfWork = new UnitOfWork(_connection))
            {
                unitOfWork.FormasPago.Delete(id);
                unitOfWork.Commit();
            }
        }
    }
}
