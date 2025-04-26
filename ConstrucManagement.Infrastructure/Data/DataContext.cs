using ConstrucManagement.Domain.Construction;
using ConstrucManagement.Domain.Entities.Construction;
using ConstrucManagement.Domain.Entities.Finance;
using ConstrucManagement.Domain.Entities.Documentacion;
using ConstrucManagement.Domain.Entities.Resources;
using Microsoft.EntityFrameworkCore;
using Proyecto = ConstrucManagement.Domain.Construction.Proyecto;
using ConstrucManagement.Domain.Enums;


namespace ConstrucManagement.Infrastructure.Data
{
    public class ConstructionDbContext : DbContext
    {
        public ConstructionDbContext(DbContextOptions<ConstructionDbContext> options)
            : base(options) { }

        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<EtapaConstruccion> EtapasConstruccion { get; set; }
        public DbSet<AsignacionRecurso> AsignacionesRecursos { get; set; }

        public DbSet<Recurso> Recursos { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Personal> Personal { get; set; }

        public DbSet<Presupuesto> Presupuestos { get; set; }
        public DbSet<RubroPresupuestario> RubrosPresupuestarios { get; set; }
        public DbSet<Gasto> Gastos { get; set; }

        public DbSet<Documento> Documentos { get; set; }
        public DbSet<VersionDocumento> VersionesDocumentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recurso>()
           .HasDiscriminator<TipoRecurso>("TipoRecurso")
           .HasValue<Material>(TipoRecurso.Material)
           .HasValue<Equipo>(TipoRecurso.Equipo)
           .HasValue<Personal>(TipoRecurso.Personal);

            modelBuilder.Entity<Proyecto>(static entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasMany(static p => p.Etapas)
                    .WithOne(e => e.Proyecto)
                    .HasForeignKey(e => e.ProyectoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EtapaConstruccion>(entity =>
            {
                entity.HasMany(e => e.EtapasPredecesoras)
                    .WithMany()
                    .UsingEntity<Dictionary<string, object>>(
                        "EtapaPredecesoras",
                        j => j.HasOne<EtapaConstruccion>().WithMany().HasForeignKey("PredecesoraId"),
                        j => j.HasOne<EtapaConstruccion>().WithMany().HasForeignKey("EtapaId")
                    );
            });

            modelBuilder.Entity<Gasto>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.HasOne(g => g.Etapa)
                      .WithMany(e => e.Gastos)
                      .HasForeignKey(g => g.EtapaId);
            });

            modelBuilder.Entity<AsignacionRecurso>(entity =>
            {
                entity.HasOne(a => a.Etapa)
                    .WithMany(e => e.AsignacionesRecursos)
                    .HasForeignKey(a => a.EtapaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.Recurso)
                    .WithMany(r => r.Asignaciones)
                    .HasForeignKey(a => a.RecursoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Documento>(entity =>
            {
                entity.HasMany(d => d.Versiones)
                    .WithOne(v => v.Documento)
                    .HasForeignKey(v => v.DocumentoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}