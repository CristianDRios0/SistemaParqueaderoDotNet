namespace SistemaParqueadero.Models
{
    public enum TipoCelda
    {
        Moto,
        Automovil
    }

    public enum EstadoCelda
    {
        Libre,
        Ocupado,
        Reservado
    }
    public class Celda
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public TipoCelda Tipo { get; set; }
        public EstadoCelda Estado { get; set; }
    }
}
