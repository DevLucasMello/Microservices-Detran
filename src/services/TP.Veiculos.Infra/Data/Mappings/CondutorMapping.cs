using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TP.Veiculos.Domain;

namespace TP.Veiculos.Infra.Data.Mappings
{
    public class CondutorMapping : IEntityTypeConfiguration<Condutor>
    {
        public void Configure(EntityTypeBuilder<Condutor> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .HasOne(x => x.Veiculo)
                .WithMany(x => x.Condutor)
                .HasConstraintName("FK_Veiculo_Condutor")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.VeiculoId)
                .IsRequired()
                .HasColumnName("VeiculoId");

            builder.Property(c => c.CondutorId)
                .IsRequired()
                .HasColumnName("CondutorId")
                .HasColumnType("varchar(40)");            

            builder.Property(c => c.CPF)
                .IsRequired()
                .HasColumnName("CPF")
                .HasColumnType("varchar(20)");

            builder.ToTable("Condutor");
        }
    }
}