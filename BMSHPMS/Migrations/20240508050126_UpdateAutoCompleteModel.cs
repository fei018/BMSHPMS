using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMSHPMS.Migrations
{
    public partial class UpdateAutoCompleteModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Info_AutoComplete",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info_AutoComplete", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Info_AutoComplete");
        }
    }
}
