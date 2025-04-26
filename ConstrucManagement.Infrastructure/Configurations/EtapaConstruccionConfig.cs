using ConstrucManagement.Domain.Entities.Construction;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructoManagement.Infrastructure.Configurations
{
    public class EtapaConstruccionConfig : IEntityTypeConfiguration<EtapaConstruccion>
    {
        public void Configure(EntityTypeBuilder<EtapaConstruccion> builder)
        {
            builder.ToTable("EtapasConstruccion", "construction");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.FechaInicioPlaneada)
                .IsRequired();

            builder.Property(e => e.FechaFinPlaneada)
                .IsRequired();

            builder.Property(e => e.CostoEstimado)
                .HasColumnType("decimal(18,2)");

            builder.HasMany(e => e.AsignacionesRecursos)
                .WithOne(a => a.EtapaConstruccion)
                .HasForeignKey(a => a.EtapaId);
        }
    }
}
