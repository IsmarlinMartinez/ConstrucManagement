using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConstrucManagement.Domain.Entities.Construction;

namespace ConstrucManagement.Infrastructure.Configurations
{
    public class ProyectoConfig : IEntityTypeConfiguration<Proyecto>
    {
        public void Configure(EntityTypeBuilder<Proyecto> builder)
        {
            builder.ToTable("Proyectos", "construction");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Descripcion)
                .HasMaxLength(500);

            builder.Property(p => p.Ubicacion)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.PresupuestoInicial)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Estado)
                .HasConversion<string>()
                .IsRequired();

            builder.HasMany(p => p.Etapas)
                .WithOne(e => e.Proyecto)
                .HasForeignKey(e => e.ProyectoId);
        }
    }
}