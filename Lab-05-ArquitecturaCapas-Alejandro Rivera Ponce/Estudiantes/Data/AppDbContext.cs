using Microsoft.EntityFrameworkCore;
using Estudiantes.Models;

namespace Estudiantes.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Estudiante> Estudiantes { get; set; }
    public DbSet<Materia> Materias { get; set; }
}