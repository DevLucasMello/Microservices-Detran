using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using TP.Veiculos.Domain;

namespace TP.Veiculos.Infra.Data.Mappings
{
    public class VeiculoMapping : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Placa)
               .IsRequired()
               .HasColumnName("Placa")
               .HasColumnType("varchar(20)");

            builder.Property(c => c.Modelo)
               .IsRequired()
               .HasColumnName("Modelo")
               .HasColumnType("varchar(50)");

            builder.Property(c => c.Marca)
               .IsRequired()
               .HasColumnName("Marca")
               .HasColumnType("varchar(50)");

            builder.Property(c => c.Cor)
               .IsRequired()
               .HasColumnName("Cor")
               .HasColumnType("varchar(50)");

            builder.Property(c => c.AnoFabricacao)
               .IsRequired()
               .HasColumnName("AnoFabricacao")
               .HasColumnType("int");

            builder
                .HasMany(p => p.Condutor)
                .WithMany(p => p.Veiculo)
                .UsingEntity<Dictionary<string, object>>(
                    "CondutoresVeiculos",
                    p => p.HasOne<CondutorVeiculo>().WithMany().HasForeignKey("VeiculoId"),
                    p => p.HasOne<Veiculo>().WithMany().HasForeignKey("CondutorId")
                    
                );

            builder.ToTable("Veiculo");
        }
    }
}
