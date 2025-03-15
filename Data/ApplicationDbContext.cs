using Microsoft.EntityFrameworkCore;
using SistemaParqueadero.Models;

namespace SistemaParqueadero.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Celda> Celdas { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<Parqueo> Parqueos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
    }
}
