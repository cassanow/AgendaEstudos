using Microsoft.EntityFrameworkCore;

namespace AgendaEstudos.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) {}
}