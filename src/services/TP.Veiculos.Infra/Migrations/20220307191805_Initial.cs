using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TP.Veiculos.Infra.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Condutor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CPF = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condutor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Placa = table.Column<string>(type: "varchar(20)", nullable: false),
                    Modelo = table.Column<string>(type: "varchar(50)", nullable: false),
                    Marca = table.Column<string>(type: "varchar(50)", nullable: false),
                    Cor = table.Column<string>(type: "varchar(50)", nullable: false),
                    AnoFabricacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CondutoresVeiculos",
                columns: table => new
                {
                    CondutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VeiculoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondutoresVeiculos", x => new { x.CondutorId, x.VeiculoId });
                    table.ForeignKey(
                        name: "FK_CondutoresVeiculos_Condutor_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Condutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CondutoresVeiculos_Veiculo_CondutorId",
                        column: x => x.CondutorId,
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CondutoresVeiculos_VeiculoId",
                table: "CondutoresVeiculos",
                column: "VeiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CondutoresVeiculos");

            migrationBuilder.DropTable(
                name: "Condutor");

            migrationBuilder.DropTable(
                name: "Veiculo");
        }
    }
}
