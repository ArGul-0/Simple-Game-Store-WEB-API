using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Simple_Game_Store_WEB_API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserLibraryPlusFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGame_GameDetailsDTO_GameID",
                table: "UserGame");

            migrationBuilder.DropTable(
                name: "GameDetailsDTO");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGame_Games_GameID",
                table: "UserGame",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGame_Games_GameID",
                table: "UserGame");

            migrationBuilder.CreateTable(
                name: "GameDetailsDTO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GenreID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDetailsDTO", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserGame_GameDetailsDTO_GameID",
                table: "UserGame",
                column: "GameID",
                principalTable: "GameDetailsDTO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
