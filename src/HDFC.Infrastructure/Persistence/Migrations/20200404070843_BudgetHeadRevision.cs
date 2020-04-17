using Microsoft.EntityFrameworkCore.Migrations;

namespace HDFC.Infrastructure.Persistence.Migrations
{
    public partial class BudgetHeadRevision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "BudgetHeads");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                table: "BudgetHeads");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "BudgetHeads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "BudgetHeads");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BudgetHeads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceId",
                table: "BudgetHeads",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
