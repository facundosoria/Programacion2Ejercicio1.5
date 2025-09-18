using Ejercicio1_5.Domain;
using Ejercicio1_5.Data;


namespace Ejercicio1_5.Servicie
{
    public class ServiceFormaPago : IServiceFormaPago
    {
        private readonly AppDbContext _context;

        public ServiceFormaPago(AppDbContext context)
        {
            _context = context;
        }

        public List<FormaPago> GetAll()
        {
            return _context.FormaPagos.ToList();
        }

        public FormaPago? GetById(int id)
        {
            return _context.FormaPagos.Find(id);
        }

        public void Add(FormaPago formaPago)
        {
            _context.FormaPagos.Add(formaPago);
            _context.SaveChanges();
        }

        public void Update(FormaPago formaPago)
        {
            _context.FormaPagos.Update(formaPago);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var formaPago = _context.FormaPagos.Find(id);
            if (formaPago != null)
            {
                _context.FormaPagos.Remove(formaPago);
                _context.SaveChanges();
            }
        }
    }
}