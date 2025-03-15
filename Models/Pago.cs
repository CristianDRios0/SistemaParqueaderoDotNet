using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaParqueadero.Models
{
    [Table("pago")]
    public class Pago
    {
        public int Id { get; set; }
        public int? ParqueoId { get; set; }
        public int? ClienteId { get; set; }
        public int Monto { get; set; }

        //relaciones con las Foreign Keys

        public Parqueo? Parqueo { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
