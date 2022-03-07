using Microsoft.EntityFrameworkCore.Migrations;

namespace TP.Condutores.Infra.Migrations
{
    public partial class veiculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VeiculoId",
                table: "Veiculo",
                type: "varchar(40)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VeiculoId",
                table: "Veiculo");
        }
    }
}
