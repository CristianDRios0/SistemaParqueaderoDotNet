namespace SistemaParqueadero.Models
{
    public enum EstadoParqueo
    {
        Activo,
        Finalizado
    }
    public class Parqueo
    {
        public int Id { get; set; }
        public int VehiculoId { get; set; }
        public int CeldaId { get; set; }
        public int TarifaId { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public int? TotalPagado { get; set; }
        public EstadoParqueo Estado { get; set; }

        //relaciones con las Foreign Keys
        public Vehiculo Vehiculo { get; set; }
        public Celda Celda { get; set; }
        public Tarifa Tarifa { get; set; }
    }
}
