using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMSHPMS.Migrations
{
    public partial class UpdateRollbackName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DSRegRollback",
                table: "DSRegRollback");

            migrationBuilder.RenameTable(
                name: "DSRegRollback",
                newName: "Reg_RollbackInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reg_RollbackInfo",
                table: "Reg_RollbackInfo",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reg_RollbackInfo",
                table: "Reg_RollbackInfo");

            migrationBuilder.RenameTable(
                name: "Reg_RollbackInfo",
                newName: "DSRegRollback");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DSRegRollback",
                table: "DSRegRollback",
                column: "ID");
        }
    }
}
