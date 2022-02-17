using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TP.Condutores.Domain;

namespace TP.Condutores.Infra.Data.Mappings
{
    public class VeiculoMapping : IEntityTypeConfiguration<VeiculoCondutor>
    {
        public void Configure(EntityTypeBuilder<VeiculoCondutor> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CondutorId)
                .IsRequired()
                .HasColumnName("CondutorId")
                .HasColumnType("varchar(40)");

            builder.Property(c => c.Placa)
                .IsRequired()
                .HasColumnName("Placa")
                .HasColumnType("varchar(20)");            

            builder.ToTable("Veiculo");
        }
    }
}
