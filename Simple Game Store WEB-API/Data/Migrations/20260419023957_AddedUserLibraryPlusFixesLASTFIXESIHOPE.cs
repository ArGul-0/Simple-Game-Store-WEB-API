using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simple_Game_Store_WEB_API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserLibraryPlusFixesLASTFIXESIHOPE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGame_Games_GameID",
                table: "UserGame");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGame_UserLibraries_UserLibraryID",
                table: "UserGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGame",
                table: "UserGame");

            migrationBuilder.DropColumn(
                name: "LibraryID",
                table: "UserGame");

            migrationBuilder.RenameTable(
                name: "UserGame",
                newName: "UserGames");

            migrationBuilder.RenameIndex(
                name: "IX_UserGame_UserLibraryID",
                table: "UserGames",
                newName: "IX_UserGames_UserLibraryID");

            migrationBuilder.RenameIndex(
                name: "IX_UserGame_GameID",
                table: "UserGames",
                newName: "IX_UserGames_GameID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGames",
                table: "UserGames",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGames_Games_GameID",
                table: "UserGames",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGames_UserLibraries_UserLibraryID",
                table: "UserGames",
                column: "UserLibraryID",
                principalTable: "UserLibraries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGames_Games_GameID",
                table: "UserGames");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGames_UserLibraries_UserLibraryID",
                table: "UserGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGames",
                table: "UserGames");

            migrationBuilder.RenameTable(
                name: "UserGames",
                newName: "UserGame");

            migrationBuilder.RenameIndex(
                name: "IX_UserGames_UserLibraryID",
                table: "UserGame",
                newName: "IX_UserGame_UserLibraryID");

            migrationBuilder.RenameIndex(
                name: "IX_UserGames_GameID",
                table: "UserGame",
                newName: "IX_UserGame_GameID");

            migrationBuilder.AddColumn<int>(
                name: "LibraryID",
                table: "UserGame",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGame",
                table: "UserGame",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGame_Games_GameID",
                table: "UserGame",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGame_UserLibraries_UserLibraryID",
                table: "UserGame",
                column: "UserLibraryID",
                principalTable: "UserLibraries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
