using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMSHPMS.Migrations
{
    public partial class AddCommonModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Info_CommonReceipt",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiptNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sum = table.Column<int>(type: "int", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonationCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info_CommonReceipt", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Info_AnnualDabei",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sum = table.Column<int>(type: "int", nullable: false),
                    CommonReceiptId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info_AnnualDabei", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Info_AnnualDabei_Info_CommonReceipt_CommonReceiptId",
                        column: x => x.CommonReceiptId,
                        principalTable: "Info_CommonReceipt",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Info_AnnualLight",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sum = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    WishNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonateLightMode = table.Column<int>(type: "int", nullable: false),
                    ContactAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommonReceiptId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info_AnnualLight", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Info_AnnualLight_Info_CommonReceipt_CommonReceiptId",
                        column: x => x.CommonReceiptId,
                        principalTable: "Info_CommonReceipt",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Info_AnnualDabei_CommonReceiptId",
                table: "Info_AnnualDabei",
                column: "CommonReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_Info_AnnualLight_CommonReceiptId",
                table: "Info_AnnualLight",
                column: "CommonReceiptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Info_AnnualDabei");

            migrationBuilder.DropTable(
                name: "Info_AnnualLight");

            migrationBuilder.DropTable(
                name: "Info_CommonReceipt");
        }
    }
}
