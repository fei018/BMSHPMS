using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMSHPMS.Migrations
{
    public partial class ModifyInfoDonationProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DProjectSerial",
                table: "Info_Memorial",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DProjectSerialNumber",
                table: "Info_Memorial",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DonationProjectId",
                table: "Info_Memorial",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DProjectSerial",
                table: "Info_Longevity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DProjectSerialNumber",
                table: "Info_Longevity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DonationProjectId",
                table: "Info_Longevity",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DProjectSerial",
                table: "Info_Donor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DProjectSerialNumber",
                table: "Info_Donor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DonationProjectId",
                table: "Info_Donor",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DProjectSerial",
                table: "Info_Memorial");

            migrationBuilder.DropColumn(
                name: "DProjectSerialNumber",
                table: "Info_Memorial");

            migrationBuilder.DropColumn(
                name: "DonationProjectId",
                table: "Info_Memorial");

            migrationBuilder.DropColumn(
                name: "DProjectSerial",
                table: "Info_Longevity");

            migrationBuilder.DropColumn(
                name: "DProjectSerialNumber",
                table: "Info_Longevity");

            migrationBuilder.DropColumn(
                name: "DonationProjectId",
                table: "Info_Longevity");

            migrationBuilder.DropColumn(
                name: "DProjectSerial",
                table: "Info_Donor");

            migrationBuilder.DropColumn(
                name: "DProjectSerialNumber",
                table: "Info_Donor");

            migrationBuilder.DropColumn(
                name: "DonationProjectId",
                table: "Info_Donor");
        }
    }
}
