using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HDFC.Infrastructure.Persistence.Migrations
{
    public partial class BudgetMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<long>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Status = table.Column<string>(type: "char(50)", nullable: false),
                    BudgetHeadId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: true),
                    CostCodeId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BudgetSpendLimits",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    SpendLimit = table.Column<decimal>(type: "decimal(19, 4)", nullable: false),
                    BudgetId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetSpendLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetSpendLimits_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubBudgets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    BudgetAmount = table.Column<decimal>(type: "decimal(19, 4)", nullable: false),
                    TransactionType = table.Column<string>(nullable: true),
                    BudgetId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubBudgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubBudgets_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetSpendLimits_BudgetId",
                table: "BudgetSpendLimits",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_SubBudgets_BudgetId",
                table: "SubBudgets",
                column: "BudgetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetSpendLimits");

            migrationBuilder.DropTable(
                name: "SubBudgets");

            migrationBuilder.DropTable(
                name: "Budgets");
        }
    }
}
