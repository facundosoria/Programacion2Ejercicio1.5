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


    public class DetalleFacturaCreateDto
    {
        public int NroFactura { get; set; }
        public List<ArticuloAddDto> Articulos { get; set; } = new List<ArticuloAddDto>();
        public int Cantidad { get; set; }
    }


}
