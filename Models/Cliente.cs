using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SistemaParqueadero.Models
{
    public enum TipoPlan
    {
        Mensual,
        Ocasional
    }
    [Table("cliente")]
    public class Cliente
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public int Identificacion { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))] // Convierte el enum en JSON
        [Column(TypeName = "nvarchar(20)")]
        public TipoPlan TipoPlan { get; set;  }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
