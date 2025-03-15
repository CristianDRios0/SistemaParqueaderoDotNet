using System.ComponentModel.DataAnnotations.Schema;

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
    [Table("celda")]
    public class Celda
    {
        public int Id { get; set; }
        public required string Codigo { get; set; }
        public TipoCelda Tipo { get; set; }
        public EstadoCelda Estado { get; set; }
    }
}
