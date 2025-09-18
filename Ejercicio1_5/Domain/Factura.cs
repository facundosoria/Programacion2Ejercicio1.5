using System;
using System.Collections.Generic;

namespace Ejercicio1_5.Domain;

public partial class Factura
{
    public int NroFactura { get; set; }

    public DateOnly Fecha { get; set; }

    public int IdFormaPago { get; set; }

    public string Cliente { get; set; } = null!;

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual FormaPago IdFormaPagoNavigation { get; set; } = null!;
}
