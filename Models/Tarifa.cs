using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SistemaParqueadero.Models
{
    public enum TipoTarifa
    {
        Hora,
        Mensual
    }

    public enum TipoVehiculo
    {
        Moto,
        Automovil
    }
    [Table("tarifa")]
    public class Tarifa
    {
        public int Id { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Column(TypeName = "nvarchar(10)")]
        public TipoTarifa Tipo { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Column(TypeName = "nvarchar(10)")]
        public TipoVehiculo VehiculoTipo { get; set; }
        public int Monto { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
