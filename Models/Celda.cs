using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonConverter(typeof(JsonStringEnumConverter))] 
        [Column(TypeName = "nvarchar(10)")]
        public TipoCelda Tipo { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Column(TypeName = "nvarchar(10)")]
        public EstadoCelda Estado { get; set; }
    }
}
