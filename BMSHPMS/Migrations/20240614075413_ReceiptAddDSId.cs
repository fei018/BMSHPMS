using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMSHPMS.Migrations
{
    public partial class ReceiptAddDSId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DharmaServiceId",
                table: "Info_Receipt",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DharmaServiceId",
                table: "Info_Receipt");
        }
    }
}
