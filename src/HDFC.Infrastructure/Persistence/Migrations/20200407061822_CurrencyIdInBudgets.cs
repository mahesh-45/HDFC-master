using Microsoft.EntityFrameworkCore.Migrations;

namespace HDFC.Infrastructure.Persistence.Migrations
{
    public partial class CurrencyIdInBudgets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Budgets",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Budgets");
        }
    }
}
