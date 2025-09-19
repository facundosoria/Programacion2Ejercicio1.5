namespace Ejercicio1_5.Api.Dto
{

    public class DetalleFacturaSimpleDto
    {

        public ArticuloSimpleDto Articulo { get; set; }
        public int Cantidad { get; set; }
    }


    public class DetalleFacturaFullDto
    {
        public int IdDetalle { get; set; }
        public int NroFactura { get; set; }
        public List<ArticuloFullDto> Articulos { get; set; } = new List<ArticuloFullDto>();
        public int Cantidad { get; set; }
    }


    public class DetalleFacturaAddDto
    {
        public int NroFactura { get; set; }
        public ArticuloAddDto Articulo { get; set; } = new ArticuloAddDto();
        public int Cantidad { get; set; }
    }
}
