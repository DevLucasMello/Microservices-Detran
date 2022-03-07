using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TP.Condutores.Domain;

namespace TP.Condutores.Infra.Data.Mappings
{
    public class VeiculoMapping : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .HasOne(x => x.Condutor)
                .WithMany(x => x.Veiculo)
                .HasConstraintName("FK_Condutor_Veiculo")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.CondutorId)
                .IsRequired()
                .HasColumnName("CondutorId");

            builder.Property(c => c.IdVeiculo)
                .IsRequired()
                .HasColumnName("VeiculoId")
                .HasColumnType("varchar(40)");

            builder.Property(c => c.Placa)
                .IsRequired()
                .HasColumnName("Placa")
                .HasColumnType("varchar(20)");

            builder.ToTable("Veiculo");
        }
    }
}
