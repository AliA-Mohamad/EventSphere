using Microsoft.EntityFrameworkCore;

namespace EventSphere.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<> Usuarios { get; set; }
    public DbSet<> Eventos { get; set; }

}
