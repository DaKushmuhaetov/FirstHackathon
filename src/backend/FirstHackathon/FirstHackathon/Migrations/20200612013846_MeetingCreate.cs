using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstHackathon.Migrations
{
    public partial class MeetingCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "Meetings");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Meetings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Meetings");

            migrationBuilder.AddColumn<byte[]>(
                name: "Body",
                table: "Meetings",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
