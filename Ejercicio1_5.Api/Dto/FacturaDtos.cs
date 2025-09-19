namespace Ejercicio1_5.Api.Dto
{
    public class FacturaSimpleDto
    {
        public int NroFactura { get; set; }
        public DateOnly Fecha { get; set; }
        public string FormaPago { get; set; }
        public string Cliente { get; set; }
        public List<DetalleFacturaSimpleDto> Detalles { get; set; } = new List<DetalleFacturaSimpleDto>();
    }

    public class FacturaFullDto
    {
        public int NroFactura { get; set; }
        public DateOnly Fecha { get; set; }
        public FormaPagoFullDto? FormaPago { get; set; }
        public string Cliente { get; set; }
        public List<DetalleFacturaFullDto> Detalles { get; set; } = new List<DetalleFacturaFullDto>();
    }

    public class FacturaAddDto
    {
        public DateOnly Fecha { get; set; }
        public int IdFormaPago { get; set; }
        public string Cliente { get; set; }
        public List<DetalleFacturaAddDto> Detalles { get; set; } = new List<DetalleFacturaAddDto>();
    }

    public class FacturaUpdateDto
    {
        public int NroFactura { get; set; }
        public DateOnly Fecha { get; set; }
        public int IdFormaPago { get; set; }
        public string Cliente { get; set; }
        public List<DetalleFacturaAddDto> Detalles { get; set; } = new List<DetalleFacturaAddDto>();
        
    }
}