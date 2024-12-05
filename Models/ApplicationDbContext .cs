using EventosAPI.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurações adicionais podem ser feitas aqui, se necessário
    }
    public DbSet<TbUsuario> tbUsuario { get; set; }
    public DbSet<TbEvento> tbEvento { get; set; }
    public DbSet<TbParticipante> TbParticipante { get; set; }

}
