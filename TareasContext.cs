using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PROYECTOEF.Models;

namespace PROYECTOEF;

    public class TareasContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }    
        public DbSet<Tarea> Tareas { get; set; }
    public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region categoria
        List<Categoria> categoriaInit = new List<Categoria>();
        categoriaInit.Add(new Categoria() { CategoriaId = Guid.Parse("3fe148ff-c319-4f50-bb97-ac9a5943bbd2"), Nombre = "Actividades pendientes", Peso = 20 });
        categoriaInit.Add(new Categoria() { CategoriaId = Guid.Parse("3fe148ff-c319-4f50-bb97-ac9a5943bb02"), Nombre = "Actividades personales", Peso = 50 });
        
        modelBuilder.Entity<Categoria>(categoria =>
        {
            categoria.ToTable("Categoria");
            categoria.HasKey(p => p.CategoriaId);
            categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(p => p.Descripcion).IsRequired(false);
            categoria.Property(p => p.Peso);
            categoria.HasData(categoriaInit);
        });
        #endregion categoria

        List<Tarea> TareasInit = new List<Tarea>();
        TareasInit.Add(new Tarea() { TareaId = Guid.Parse("3fe148ff-c319-4f50-bb97-ac9a5943bb10"), CategoriaId = Guid.Parse("3fe148ff-c319-4f50-bb97-ac9a5943bbd2"), PrioridadTarea = Prioridad.Media, Titulo = "Pago de servicios publicos", FechaCreacion = DateTime.Now });
        TareasInit.Add(new Tarea() { TareaId = Guid.Parse("3fe148ff-c319-4f50-bb97-ac9a5943bb10"), CategoriaId = Guid.Parse("3fe148ff-c319-4f50-bb97-ac9a5943bb02"), PrioridadTarea = Prioridad.Baja, Titulo = "Terminar de ver película en Netflix", FechaCreacion = DateTime.Now });


        modelBuilder.Entity<Tarea>(tarea =>
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(p => p.TareaId);
            tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
            tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
            tarea.Property(p => p.Descripcion).IsRequired(false);
            tarea.Property(p => p.PrioridadTarea);
            tarea.Property(p => p.FechaCreacion);
            tarea.Ignore(p => p.Resumen);
            tarea.HasData(TareasInit);
            

        });
    }
    }