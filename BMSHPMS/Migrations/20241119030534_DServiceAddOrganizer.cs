using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMSHPMS.Migrations
{
    public partial class DServiceAddOrganizer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UsedNumber",
                table: "Opt_DonationProject",
                type: "int",
                nullable: false,
                comment: "編號計數",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "編號已計數");

            migrationBuilder.AddColumn<string>(
                name: "ServiceDateDescription",
                table: "Opt_DharmaService",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceOrganizer",
                table: "Opt_DharmaService",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptOwn",
                table: "Info_Receipt",
                type: "nvarchar(max)",
                nullable: true,
                comment: "收據人名",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "收據人姓名");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceDateDescription",
                table: "Opt_DharmaService");

            migrationBuilder.DropColumn(
                name: "ServiceOrganizer",
                table: "Opt_DharmaService");

            migrationBuilder.AlterColumn<int>(
                name: "UsedNumber",
                table: "Opt_DonationProject",
                type: "int",
                nullable: false,
                comment: "編號已計數",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "編號計數");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptOwn",
                table: "Info_Receipt",
                type: "nvarchar(max)",
                nullable: true,
                comment: "收據人姓名",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "收據人名");
        }
    }
}
