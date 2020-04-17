using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HDFC.Infrastructure.Persistence.Migrations
{
    public partial class BudgetoryStructureChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "CostCodeId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Budgets");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "BudgetSpendLimits",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DepartmentId",
                table: "Budgets",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BasicAmount",
                table: "Budgets",
                type: "decimal(19, 4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "BudgetCategoryId",
                table: "Budgets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "BudgetStatus",
                table: "Budgets",
                type: "char(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BudgetType",
                table: "Budgets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Budgets",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DivisionId",
                table: "Budgets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Budgets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Justification",
                table: "Budgets",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PersonInChargeId",
                table: "Budgets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Budgets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "SubDivisionId",
                table: "Budgets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                table: "Budgets",
                type: "decimal(19, 4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Budgets",
                type: "decimal(19, 4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Budgets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "BudgetCostCodes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostCodeId = table.Column<long>(nullable: false),
                    BudgetId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetCostCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetCostCodes_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCostCodes_BudgetId",
                table: "BudgetCostCodes",
                column: "BudgetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetCostCodes");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "BudgetSpendLimits");

            migrationBuilder.DropColumn(
                name: "BasicAmount",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "BudgetCategoryId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "BudgetStatus",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "BudgetType",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "Justification",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "PersonInChargeId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "SubDivisionId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Budgets");

            migrationBuilder.AlterColumn<long>(
                name: "DepartmentId",
                table: "Budgets",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Budgets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CostCodeId",
                table: "Budgets",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Budgets",
                type: "char(50)",
                nullable: false,
                defaultValue: "");
        }
    }
}
