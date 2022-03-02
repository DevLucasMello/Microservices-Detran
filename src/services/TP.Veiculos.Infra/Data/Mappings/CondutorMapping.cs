using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TP.Veiculos.Domain;

namespace TP.Veiculos.Infra.Data.Mappings
{
    public class CondutorMapping : IEntityTypeConfiguration<CondutorVeiculo>
    {
        public void Configure(EntityTypeBuilder<CondutorVeiculo> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.VeiculoId)
                .IsRequired()
                .HasColumnName("VeiculoId")
                .HasColumnType("varchar(40)");

            builder.Property(c => c.CPF)
                .IsRequired()
                .HasColumnName("CPF")
                .HasColumnType("varchar(20)");

            builder.ToTable("Condutor");
        }
    }
}