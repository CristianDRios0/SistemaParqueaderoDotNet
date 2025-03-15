using System.ComponentModel.DataAnnotations.Schema;

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
        public TipoTarifa Tipo { get; set; }
        public TipoVehiculo VehiculoTipo { get; set; }
        public int Monto { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
