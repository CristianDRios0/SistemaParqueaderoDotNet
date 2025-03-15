namespace SistemaParqueadero.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public int? ClienteId { get; set; }
        public string Placa { get; set; }

        //relaciones con las Foreign Keys
        public Cliente? Cliente { get; set; }
    }
}
