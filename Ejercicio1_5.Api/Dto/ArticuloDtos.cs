namespace Ejercicio1_5.Api.Dto
{

    public class ArticuloSimpleDto
    {
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class ArticuloFullDto
    {
        public int IdArticulo { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
    }


    public class ArticuloAddDto
    {
        public int IdArticulo { get; set; }
    }
}   