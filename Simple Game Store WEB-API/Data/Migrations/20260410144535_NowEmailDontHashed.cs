using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simple_Game_Store_WEB_API.Data.Migrations
{
    /// <inheritdoc />
    public partial class NowEmailDontHashed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashedEmail",
                table: "Users",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "HashedEmail");
        }
    }
}
