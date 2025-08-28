using Ejercicio1_5.Domain;
using FormaPagoEntity = Ejercicio1_5.Domain.FormaPago;
using System.Collections.Generic;
using Ejercicio1_5.Data.Conection;

namespace Ejercicio1_5.Data.FormaPago
{
    public class FormaPagoRepository : IFormaPagoRepository
    {
        private IDataHelper _dataHelper;

        public FormaPagoRepository(IDataHelper dataHelper)
        {
            _dataHelper = dataHelper;
        }

        public List<FormaPagoEntity> GetAll()
        {
            var formaPagos = new List<FormaPagoEntity>();
            var dt = _dataHelper.ExecuteSP("GetAllFormaPagos");
            foreach (System.Data.DataRow row in dt.Rows)
            {
                formaPagos.Add(new FormaPagoEntity
                {
                    IdFormaPago = Convert.ToInt32(row["IdFormaPago"]),
                    Nombre = row["Nombre"].ToString(),
                });
            }
            return formaPagos;
        }

    public Domain.FormaPago GetById(int id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@IdFormaPago", id }
            };
            var dt = _dataHelper.ExecuteSP("GetFormaPagoById", parameters);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                return new FormaPagoEntity
                {
                    IdFormaPago = Convert.ToInt32(row["IdFormaPago"]),
                    Nombre = row["Nombre"].ToString(),
                };
            }
            return null;
        }


    public void Add(Domain.FormaPago formaPago)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Nombre", formaPago.Nombre }
            };
            _dataHelper.ExecuteSP("AddFormaPago", parameters);
        }

    public void Update(Domain.FormaPago formaPago)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@IdFormaPago", formaPago.IdFormaPago },
                { "@Nombre", formaPago.Nombre }
            };
            _dataHelper.ExecuteSP("UpdateFormaPago", parameters);
        }

        public void Delete(int id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@IdFormaPago", id }
            };
            _dataHelper.ExecuteSP("DeleteFormaPago", parameters);
        }
    }
}

