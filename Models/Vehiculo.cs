using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaParqueadero.Models
{
    [Table("vehiculo")]
    public class Vehiculo
    {
        public int Id { get; set; }
        public int? ClienteId { get; set; }
        public required string Placa { get; set; }

        //relaciones con las Foreign Keys
        public Cliente? Cliente { get; set; }
    }
}
