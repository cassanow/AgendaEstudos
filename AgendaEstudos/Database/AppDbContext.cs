using AgendaEstudos.Model;
using Microsoft.EntityFrameworkCore;

namespace AgendaEstudos.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) {}
    
    public DbSet<User> User { get; set; }
    
    public DbSet<Materia> Materia { get; set; }
    
    public DbSet<Tarefa> Tarefa { get; set; }   
}