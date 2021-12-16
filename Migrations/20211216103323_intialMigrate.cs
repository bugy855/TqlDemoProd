using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwilioJokeTeller.Migrations
{
    public partial class intialMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jokes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jokes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscribeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VerificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Jokes",
                columns: new[] { "ID", "Answer", "Question" },
                values: new object[,]
                {
                    { 1, "Because it was too tired!", "Why did the bike go to sleep early?" },
                    { 2, "You hit rock bottom!", "What happens when you slap Dwayne Johnson's butt?" },
                    { 3, "Because they are full of antybodies!", "Why don't ant eaters get sick?" },
                    { 4, "Sofishticated!", "What do you call a fish wearing a bow tie?" },
                    { 5, "You look like a funguy!", "What did one mushroom say to the other mushroom?" },
                    { 6, "No seriously guys. I can't figure out how to get to the bulb. There's nothing to grab. Please send help", "How many web developers does it take to screw in a lightbulb?" },
                    { 7, "Ground beef!", "What do you call a cow with no legs?" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jokes");

            migrationBuilder.DropTable(
                name: "Subscribers");
        }
    }
}
