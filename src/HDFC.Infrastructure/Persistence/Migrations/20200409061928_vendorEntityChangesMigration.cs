using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HDFC.Infrastructure.Persistence.Migrations
{
    public partial class vendorEntityChangesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "AddressLine3",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Catalogue",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "PinCode",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "SubCatalogue",
                table: "Vendors");

            migrationBuilder.AddColumn<string>(
                name: "BusinessNature",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyType",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRegisteredUnderMSME",
                table: "Vendors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "VendorAttachments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    FileSize = table.Column<decimal>(type: "decimal(19, 4)", nullable: false),
                    UploadedDate = table.Column<DateTime>(nullable: false),
                    VendorId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorAttachments_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorBankDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountHolderName = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    BankCode = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    AccountType = table.Column<string>(type: "char(50)", nullable: false),
                    VendorId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorBankDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorBankDetails_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorMSMEDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MSMERegistrationNumber = table.Column<string>(nullable: true),
                    DateofMSMERegistration = table.Column<DateTime>(nullable: true),
                    EnterpriseUnderMSME = table.Column<string>(type: "char(50)", nullable: false),
                    VendorId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorMSMEDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorMSMEDetails_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorUsers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: true),
                    VendorId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorUsers_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendorAttachments_VendorId",
                table: "VendorAttachments",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorBankDetails_VendorId",
                table: "VendorBankDetails",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorMSMEDetails_VendorId",
                table: "VendorMSMEDetails",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorUsers_VendorId",
                table: "VendorUsers",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorAttachments");

            migrationBuilder.DropTable(
                name: "VendorBankDetails");

            migrationBuilder.DropTable(
                name: "VendorMSMEDetails");

            migrationBuilder.DropTable(
                name: "VendorUsers");

            migrationBuilder.DropColumn(
                name: "BusinessNature",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CompanyType",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "IsRegisteredUnderMSME",
                table: "Vendors");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine3",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Catalogue",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PinCode",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubCatalogue",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
