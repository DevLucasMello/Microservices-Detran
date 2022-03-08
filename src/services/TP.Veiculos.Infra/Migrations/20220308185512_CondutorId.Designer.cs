﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TP.Veiculos.Infra.Data;

namespace TP.Veiculos.Infra.Migrations
{
    [DbContext(typeof(VeiculosContext))]
    [Migration("20220308185512_CondutorId")]
    partial class CondutorId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TP.Veiculos.Domain.Condutor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("CPF");

                    b.Property<string>("CondutorId")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasColumnName("CondutorId");

                    b.Property<Guid>("VeiculoId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("VeiculoId");

                    b.HasKey("Id");

                    b.HasIndex("VeiculoId");

                    b.ToTable("Condutor");
                });

            modelBuilder.Entity("TP.Veiculos.Domain.Veiculo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AnoFabricacao")
                        .HasColumnType("int")
                        .HasColumnName("AnoFabricacao");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Cor");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Marca");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Modelo");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Placa");

                    b.HasKey("Id");

                    b.ToTable("Veiculo");
                });

            modelBuilder.Entity("TP.Veiculos.Domain.Condutor", b =>
                {
                    b.HasOne("TP.Veiculos.Domain.Veiculo", "Veiculo")
                        .WithMany("Condutor")
                        .HasForeignKey("VeiculoId")
                        .HasConstraintName("FK_Veiculo_Condutor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Veiculo");
                });

            modelBuilder.Entity("TP.Veiculos.Domain.Veiculo", b =>
                {
                    b.Navigation("Condutor");
                });
#pragma warning restore 612, 618
        }
    }
}
