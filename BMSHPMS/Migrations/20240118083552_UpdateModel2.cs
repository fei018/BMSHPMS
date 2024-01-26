using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMSHPMS.Migrations
{
    public partial class UpdateModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDataValid",
                table: "Info_Receipt");

            migrationBuilder.DropColumn(
                name: "IsDataValid",
                table: "Info_Memorial");

            migrationBuilder.DropColumn(
                name: "IsDataValid",
                table: "Info_Longevity");

            migrationBuilder.DropColumn(
                name: "IsDataValid",
                table: "Info_Donor");

            migrationBuilder.CreateTable(
                name: "Info_Receipt_del",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiptNumber = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "收據號碼"),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "收據日期"),
                    DharmaServiceYear = table.Column<int>(type: "int", nullable: true, comment: "法會年份"),
                    DharmaServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "法會名"),
                    ReceiptOwn = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "收據人姓名"),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "聯絡人姓名"),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "聯絡人電話"),
                    Sum = table.Column<int>(type: "int", nullable: true, comment: "金額"),
                    DSRemark = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "備註"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info_Receipt_del", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Info_Donor_del",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LongevityName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "延生位姓名"),
                    DeceasedName_1 = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "附薦宗親名及稱呼_1"),
                    DeceasedName_2 = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "附薦宗親名及稱呼_2"),
                    DeceasedName_3 = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "附薦宗親名及稱呼_3"),
                    BenefactorName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "陽居姓名"),
                    Sum = table.Column<int>(type: "int", nullable: true, comment: "金額"),
                    SerialCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "功德主編號"),
                    DSRemark = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "備註"),
                    Receipt_delID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "收據ID"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info_Donor_del", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Info_Donor_del_Info_Receipt_del_Receipt_delID",
                        column: x => x.Receipt_delID,
                        principalTable: "Info_Receipt_del",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Info_Longevity_del",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "姓名"),
                    Sum = table.Column<int>(type: "int", nullable: true, comment: "金額"),
                    SerialCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "延生編號"),
                    DSRemark = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "備註"),
                    Receipt_delID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "收據ID"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info_Longevity_del", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Info_Longevity_del_Info_Receipt_del_Receipt_delID",
                        column: x => x.Receipt_delID,
                        principalTable: "Info_Receipt_del",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Info_Memorial_del",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerialCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "附薦編號"),
                    BenefactorName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "陽居姓名"),
                    DeceasedName_1 = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "附薦宗親名及稱呼_1"),
                    DeceasedName_2 = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "附薦宗親名及稱呼_2"),
                    DeceasedName_3 = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "附薦宗親名及稱呼_3"),
                    Sum = table.Column<int>(type: "int", nullable: true, comment: "金額"),
                    DSRemark = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "備註"),
                    IsDataValid = table.Column<bool>(type: "bit", nullable: false, comment: "數據有效"),
                    Receipt_delID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "收據ID"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info_Memorial_del", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Info_Memorial_del_Info_Receipt_del_Receipt_delID",
                        column: x => x.Receipt_delID,
                        principalTable: "Info_Receipt_del",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Info_Donor_del_Receipt_delID",
                table: "Info_Donor_del",
                column: "Receipt_delID");

            migrationBuilder.CreateIndex(
                name: "IX_Info_Longevity_del_Receipt_delID",
                table: "Info_Longevity_del",
                column: "Receipt_delID");

            migrationBuilder.CreateIndex(
                name: "IX_Info_Memorial_del_Receipt_delID",
                table: "Info_Memorial_del",
                column: "Receipt_delID");

            migrationBuilder.CreateIndex(
                name: "IX_Info_Receipt_del_ReceiptNumber",
                table: "Info_Receipt_del",
                column: "ReceiptNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Info_Donor_del");

            migrationBuilder.DropTable(
                name: "Info_Longevity_del");

            migrationBuilder.DropTable(
                name: "Info_Memorial_del");

            migrationBuilder.DropTable(
                name: "Info_Receipt_del");

            migrationBuilder.AddColumn<bool>(
                name: "IsDataValid",
                table: "Info_Receipt",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "數據有效");

            migrationBuilder.AddColumn<bool>(
                name: "IsDataValid",
                table: "Info_Memorial",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "數據有效");

            migrationBuilder.AddColumn<bool>(
                name: "IsDataValid",
                table: "Info_Longevity",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "數據有效");

            migrationBuilder.AddColumn<bool>(
                name: "IsDataValid",
                table: "Info_Donor",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "數據有效");
        }
    }
}
