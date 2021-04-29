using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WADFC.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventTitle = table.Column<string>(type: "varchar(max)", nullable: false),
                    Completed = table.Column<string>(type: "varchar(2)", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventImage = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                });

            migrationBuilder.CreateTable(
                name: "Fighters",
                columns: table => new
                {
                    FighterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Surname = table.Column<string>(type: "varchar(100)", nullable: false),
                    Win = table.Column<int>(type: "int", nullable: false),
                    Loss = table.Column<int>(type: "int", nullable: false),
                    Draw = table.Column<int>(type: "int", nullable: false),
                    NoContest = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Reach = table.Column<int>(type: "int", nullable: false),
                    Stance = table.Column<string>(type: "varchar(20)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fighters", x => x.FighterID);
                });

            migrationBuilder.CreateTable(
                name: "Fights",
                columns: table => new
                {
                    FightID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(type: "int", nullable: true),
                    FighterAID = table.Column<int>(type: "int", nullable: true),
                    FighterBID = table.Column<int>(type: "int", nullable: true),
                    Division = table.Column<string>(type: "varchar(200)", nullable: false),
                    Winner = table.Column<string>(type: "varchar(100)", nullable: true),
                    Method = table.Column<string>(type: "varchar(100)", nullable: true),
                    Round = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fights", x => x.FightID);
                    table.ForeignKey(
                        name: "FK_Fights_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fights_Fighters_FighterAID",
                        column: x => x.FighterAID,
                        principalTable: "Fighters",
                        principalColumn: "FighterID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fights_Fighters_FighterBID",
                        column: x => x.FighterBID,
                        principalTable: "Fighters",
                        principalColumn: "FighterID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fights_EventID",
                table: "Fights",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Fights_FighterAID",
                table: "Fights",
                column: "FighterAID");

            migrationBuilder.CreateIndex(
                name: "IX_Fights_FighterBID",
                table: "Fights",
                column: "FighterBID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fights");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Fighters");
        }
    }
}
