using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMSHPMS.Migrations
{
    public partial class UpdateRollbackInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDataValid",
                table: "Info_Memorial_del");

            migrationBuilder.AlterColumn<int>(
                name: "UsedNumber",
                table: "Opt_DonationProject",
                type: "int",
                nullable: false,
                comment: "編號已計數",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "已使用數");

            migrationBuilder.AlterColumn<string>(
                name: "DharmaServiceName",
                table: "Info_Receipt",
                type: "nvarchar(max)",
                nullable: true,
                comment: "法會名",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "法會名");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_3",
                table: "Info_Memorial_del",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦名稱_3",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦宗親名及稱呼_3");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_2",
                table: "Info_Memorial_del",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦名稱_2",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦宗親名及稱呼_2");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_1",
                table: "Info_Memorial_del",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦名稱_1",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦宗親名及稱呼_1");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_3",
                table: "Info_Memorial",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦名稱_3",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦宗親名及稱呼_3");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_2",
                table: "Info_Memorial",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦名稱_2",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦宗親名及稱呼_2");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_1",
                table: "Info_Memorial",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦名稱_1",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦宗親名及稱呼_1");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_3",
                table: "Info_Donor_del",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦名稱_3",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦宗親名及稱呼_3");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_2",
                table: "Info_Donor_del",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦名稱_2",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦宗親名及稱呼_2");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_1",
                table: "Info_Donor_del",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦名稱_1",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦宗親名及稱呼_1");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_3",
                table: "Info_Donor",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦名稱_3",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦宗親名及稱呼_3");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_2",
                table: "Info_Donor",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦名稱_2",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦宗親名及稱呼_2");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_1",
                table: "Info_Donor",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦名稱_1",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦宗親名及稱呼_1");

            migrationBuilder.CreateTable(
                name: "DSRegRollback",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonationProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "功德ID"),
                    PreUsedNumber = table.Column<int>(type: "int", nullable: true, comment: "功德已使用數"),
                    LastReceiptNumber = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "收據號碼")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DSRegRollback", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DSRegRollback");

            migrationBuilder.AlterColumn<int>(
                name: "UsedNumber",
                table: "Opt_DonationProject",
                type: "int",
                nullable: false,
                comment: "已使用數",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "編號已計數");

            migrationBuilder.AlterColumn<string>(
                name: "DharmaServiceName",
                table: "Info_Receipt",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "法會名",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "法會名");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_3",
                table: "Info_Memorial_del",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_3",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦名稱_3");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_2",
                table: "Info_Memorial_del",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_2",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦名稱_2");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_1",
                table: "Info_Memorial_del",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_1",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦名稱_1");

            migrationBuilder.AddColumn<bool>(
                name: "IsDataValid",
                table: "Info_Memorial_del",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "數據有效");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_3",
                table: "Info_Memorial",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_3",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦名稱_3");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_2",
                table: "Info_Memorial",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_2",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦名稱_2");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_1",
                table: "Info_Memorial",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_1",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦名稱_1");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_3",
                table: "Info_Donor_del",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_3",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦名稱_3");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_2",
                table: "Info_Donor_del",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_2",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦名稱_2");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_1",
                table: "Info_Donor_del",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_1",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦名稱_1");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_3",
                table: "Info_Donor",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_3",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦名稱_3");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_2",
                table: "Info_Donor",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_2",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦名稱_2");

            migrationBuilder.AlterColumn<string>(
                name: "DeceasedName_1",
                table: "Info_Donor",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_1",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "附薦名稱_1");
        }
    }
}
