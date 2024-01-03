using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMSHPMS.Migrations
{
    public partial class UpdateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Info_Receipt");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Info_Memorial");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Info_Longevity");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Info_Donor");

            migrationBuilder.AddColumn<bool>(
                name: "NotDeleted",
                table: "Info_Receipt",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "非刪除");

            migrationBuilder.AddColumn<bool>(
                name: "NotDeleted",
                table: "Info_Memorial",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "非刪除");

            migrationBuilder.AddColumn<bool>(
                name: "NotDeleted",
                table: "Info_Longevity",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "非刪除");

            migrationBuilder.AddColumn<bool>(
                name: "NotDeleted",
                table: "Info_Donor",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "非刪除");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotDeleted",
                table: "Info_Receipt");

            migrationBuilder.DropColumn(
                name: "NotDeleted",
                table: "Info_Memorial");

            migrationBuilder.DropColumn(
                name: "NotDeleted",
                table: "Info_Longevity");

            migrationBuilder.DropColumn(
                name: "NotDeleted",
                table: "Info_Donor");

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Info_Receipt",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Info_Memorial",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Info_Longevity",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Info_Donor",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
