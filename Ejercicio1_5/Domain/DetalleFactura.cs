using System;
using System.Collections.Generic;

namespace Ejercicio1_5.Domain;

public partial class DetalleFactura
{
    public int IdDetalle { get; set; }

    public int NroFactura { get; set; }

    public int IdArticulo { get; set; }

    public int Cantidad { get; set; }

    public virtual Articulo IdArticuloNavigation { get; set; } = null!;

    public virtual Factura NroFacturaNavigation { get; set; } = null!;
}
