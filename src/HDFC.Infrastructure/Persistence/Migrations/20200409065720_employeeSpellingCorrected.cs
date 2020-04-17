using Microsoft.EntityFrameworkCore.Migrations;

namespace HDFC.Infrastructure.Persistence.Migrations
{
    public partial class employeeSpellingCorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Employess",
                table: "Employess");

            migrationBuilder.RenameTable(
                name: "Employess",
                newName: "Employees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employess");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employess",
                table: "Employess",
                column: "Id");
        }
    }
}
