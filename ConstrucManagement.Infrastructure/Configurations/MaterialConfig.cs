using ConstrucManagement.Domain.Entities.Resources;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrucManagement.Infrastructure.Configurations
{
    public class MaterialConfig : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Materiales", "resources");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Codigo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.CostoUnitario)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(m => m.StockMinimo)
                .IsRequired();
        }
    }
}
