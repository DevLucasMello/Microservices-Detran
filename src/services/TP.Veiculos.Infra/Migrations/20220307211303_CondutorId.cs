using Microsoft.EntityFrameworkCore.Migrations;

namespace TP.Veiculos.Infra.Migrations
{
    public partial class CondutorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CondutorId",
                table: "Condutor",
                type: "varchar(40)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CondutorId",
                table: "Condutor");
        }
    }
}
