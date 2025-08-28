using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_5.Domain
{
    public class DetalleFactura
    {
        public int IdDetalle { get; set; }
        public int NroFactura { get; set; } // Clave foránea a Factura
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }
    }
}
