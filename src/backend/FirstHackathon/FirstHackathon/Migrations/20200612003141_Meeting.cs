using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace FirstHackathon.Migrations
{
    public partial class Meeting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    MeetingDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Body = table.Column<byte[]>(nullable: true),
                    HouseId = table.Column<Guid>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meetings_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_HouseId",
                table: "Meetings",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_PersonId",
                table: "Meetings",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meetings");
        }
    }
}
