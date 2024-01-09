using EventSphere.Models;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<UsuariosModel> Usuarios { get; set; }
    public DbSet<EventosModel> Eventos { get; set; }
    public DbSet<InscricoesEventoModel> InscricoesEvento { get; set;}  
}
