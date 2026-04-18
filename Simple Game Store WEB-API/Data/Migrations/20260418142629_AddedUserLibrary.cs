using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Simple_Game_Store_WEB_API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserLibrary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameDetailsDTO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    GenreID = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDetailsDTO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserLibraries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLibraries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserLibraries_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGame",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LibraryID = table.Column<int>(type: "integer", nullable: false),
                    UserLibraryID = table.Column<int>(type: "integer", nullable: false),
                    GameID = table.Column<int>(type: "integer", nullable: false),
                    PurchasedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGame", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserGame_GameDetailsDTO_GameID",
                        column: x => x.GameID,
                        principalTable: "GameDetailsDTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGame_UserLibraries_UserLibraryID",
                        column: x => x.UserLibraryID,
                        principalTable: "UserLibraries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGame_GameID",
                table: "UserGame",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_UserGame_UserLibraryID",
                table: "UserGame",
                column: "UserLibraryID");

            migrationBuilder.CreateIndex(
                name: "IX_UserLibraries_UserID",
                table: "UserLibraries",
                column: "UserID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGame");

            migrationBuilder.DropTable(
                name: "GameDetailsDTO");

            migrationBuilder.DropTable(
                name: "UserLibraries");
        }
    }
}
