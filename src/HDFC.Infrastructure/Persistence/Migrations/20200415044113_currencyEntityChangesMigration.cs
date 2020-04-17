using Microsoft.EntityFrameworkCore.Migrations;

namespace HDFC.Infrastructure.Persistence.Migrations
{
    public partial class currencyEntityChangesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Currencies");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Currencies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Currencies");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Currencies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
