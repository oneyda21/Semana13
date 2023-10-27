using GuiaSemana13;
using Microsoft.EntityFrameworkCore;
public class Context : DbContext
{
    public DbSet<Student> Estudiante { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=Progra2;");

    }
}
