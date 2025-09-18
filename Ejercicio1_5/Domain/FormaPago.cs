using System;
using System.Collections.Generic;

namespace Ejercicio1_5.Domain;

public partial class FormaPago
{
    public int IdFormaPago { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
