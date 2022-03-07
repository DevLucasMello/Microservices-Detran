using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using TP.Condutores.Domain;

namespace TP.Condutores.Infra.Data.Mappings
{
    public class CondutorMapping : IEntityTypeConfiguration<Condutor>
    {
        public void Configure(EntityTypeBuilder<Condutor> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(c => c.Nome, n =>
            {
                n.Property(c => c.PrimeiroNome)
                .IsRequired()
                .HasColumnName("PrimeiroNome")
                .HasColumnType("varchar(50)");

                n.Property(c => c.UltimoNome)
                .IsRequired()
                .HasColumnName("UltimoNome")
                .HasColumnType("varchar(50)");
            });

            builder.Property(c => c.CPF)
                .IsRequired()
                .HasColumnName("CPF")
                .HasColumnType("varchar(20)");

            builder.Property(c => c.Telefone)
                .IsRequired()
                .HasColumnName("Telefone")
                .HasColumnType("varchar(20)");

            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("varchar(100)");

            builder.Property(c => c.CNH)
                .IsRequired()
                .HasColumnName("CNH")
                .HasColumnType("varchar(20)");

            builder.Property(c => c.DataNascimento)
                .IsRequired()
                .HasColumnName("DataNascimento")
                .HasColumnType("datetime");

            builder
                .HasMany(p => p.Veiculo)
                .WithMany(p => p.Condutor)
                .UsingEntity<Dictionary<string, object>>(
                    "VeiculosCondutores",
                    p => p.HasOne<Veiculo>().WithMany().HasForeignKey("CondutorId"),
                    p => p.HasOne<Condutor>().WithMany().HasForeignKey("VeiculoId")
                );

            builder.ToTable("Condutor");
        }
    }
}