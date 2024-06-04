using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMSHPMS.Migrations
{
    public partial class AddGeneralModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Info_GeneralReceipt",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiptNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sum = table.Column<int>(type: "int", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonationCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info_GeneralReceipt", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Opt_GeneralDonationCategory",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opt_GeneralDonationCategory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Info_GeneralDonor",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sum = table.Column<int>(type: "int", nullable: false),
                    GeneralRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomCol1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomCol2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomCol3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info_GeneralDonor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Info_GeneralDonor_Info_GeneralReceipt_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Info_GeneralReceipt",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Info_GeneralDonor_ReceiptId",
                table: "Info_GeneralDonor",
                column: "ReceiptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Info_GeneralDonor");

            migrationBuilder.DropTable(
                name: "Opt_GeneralDonationCategory");

            migrationBuilder.DropTable(
                name: "Info_GeneralReceipt");
        }
    }
}
