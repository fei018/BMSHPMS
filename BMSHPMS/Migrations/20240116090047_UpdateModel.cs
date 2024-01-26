using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMSHPMS.Migrations
{
    public partial class UpdateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeceasedName",
                table: "Info_Memorial");

            migrationBuilder.DropColumn(
                name: "DeceasedName",
                table: "Info_Donor");

            migrationBuilder.AddColumn<string>(
                name: "DeceasedName_1",
                table: "Info_Memorial",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_1");

            migrationBuilder.AddColumn<string>(
                name: "DeceasedName_2",
                table: "Info_Memorial",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_2");

            migrationBuilder.AddColumn<string>(
                name: "DeceasedName_3",
                table: "Info_Memorial",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_3");

            migrationBuilder.AddColumn<string>(
                name: "DeceasedName_1",
                table: "Info_Donor",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_1");

            migrationBuilder.AddColumn<string>(
                name: "DeceasedName_2",
                table: "Info_Donor",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_2");

            migrationBuilder.AddColumn<string>(
                name: "DeceasedName_3",
                table: "Info_Donor",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼_3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeceasedName_1",
                table: "Info_Memorial");

            migrationBuilder.DropColumn(
                name: "DeceasedName_2",
                table: "Info_Memorial");

            migrationBuilder.DropColumn(
                name: "DeceasedName_3",
                table: "Info_Memorial");

            migrationBuilder.DropColumn(
                name: "DeceasedName_1",
                table: "Info_Donor");

            migrationBuilder.DropColumn(
                name: "DeceasedName_2",
                table: "Info_Donor");

            migrationBuilder.DropColumn(
                name: "DeceasedName_3",
                table: "Info_Donor");

            migrationBuilder.AddColumn<string>(
                name: "DeceasedName",
                table: "Info_Memorial",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼");

            migrationBuilder.AddColumn<string>(
                name: "DeceasedName",
                table: "Info_Donor",
                type: "nvarchar(max)",
                nullable: true,
                comment: "附薦宗親名及稱呼");
        }
    }
}
