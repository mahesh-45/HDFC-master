using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HDFC.Infrastructure.Persistence.Migrations
{
    public partial class migrationVendorCCEmp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostCodes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<long>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    BHEmpCode = table.Column<string>(nullable: true),
                    BH = table.Column<string>(nullable: true),
                    ADGroup = table.Column<string>(nullable: true),
                    ADEmpCode = table.Column<string>(nullable: true),
                    Head = table.Column<string>(nullable: true),
                    Status = table.Column<string>(type: "char(50)", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employess",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<long>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    EmployeeNumber = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    PanNumber = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    MaritalStatus = table.Column<string>(nullable: true),
                    OfficialEmail = table.Column<string>(nullable: true),
                    Contact1 = table.Column<string>(nullable: true),
                    Contact2 = table.Column<string>(nullable: true),
                    Band = table.Column<string>(nullable: true),
                    LocationName = table.Column<string>(nullable: true),
                    SupervisorEmpCode = table.Column<string>(nullable: true),
                    HireDate = table.Column<DateTime>(nullable: true),
                    RecordStatus = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    ActualTerminationDate = table.Column<DateTime>(nullable: true),
                    EmployeeStatus = table.Column<string>(type: "char(50)", nullable: false),
                    UserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
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
                    ContactPerson = table.Column<string>(nullable: true),
                    Catalogue = table.Column<string>(nullable: true),
                    SubCatalogue = table.Column<string>(nullable: true),
                    VendorType = table.Column<string>(type: "char(50)", nullable: false),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    AddressLine3 = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    LandlineNo = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PinCode = table.Column<string>(nullable: true),
                    PANNo = table.Column<string>(nullable: true),
                    PANHolderName = table.Column<string>(nullable: true),
                    AadharNo = table.Column<string>(nullable: true),
                    VendorStatus = table.Column<string>(type: "char(50)", nullable: false),
                    ProprietorName = table.Column<string>(nullable: true),
                    IsCompositeTaxable = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(type: "char(50)", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostCodes");

            migrationBuilder.DropTable(
                name: "Employess");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
