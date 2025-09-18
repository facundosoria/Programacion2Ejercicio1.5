namespace Ejercicio1_5.Api.Dto
{
    public class FormaPagoSimpleDto
    {
        public string Nombre { get; set; }
    }

    public class FormaPagoFullDto
    {
        public int IdFormaPago { get; set; }
        public string Nombre { get; set; }
    }


    public class FormaPagoAddDto
    {
        public int IdFormaPago { get; set; }
    }


}