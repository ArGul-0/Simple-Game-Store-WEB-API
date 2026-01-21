using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simple_Game_Store_WEB_API.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "ID", "GenreID", "Name", "Price", "ReleaseDate" },
                values: new object[,]
                {
                    { 1, 1, "Gta 5", 59.99m, new DateOnly(2013, 9, 17) },
                    { 2, 1, "Watch Dogs 1", 39.99m, new DateOnly(2014, 5, 26) },
                    { 3, 2, "Cities: Skylines", 29.99m, new DateOnly(2015, 3, 10) },
                    { 4, 8, "Fran Bow", 4.99m, new DateOnly(2015, 8, 27) },
                    { 5, 8, "Cry of Fear", 0m, new DateOnly(2013, 4, 25) },
                    { 6, 10, "Sally Face", 14.99m, new DateOnly(2016, 8, 16) },
                    { 7, 8, "Silent Hill 2 Remake", 29.99m, new DateOnly(2024, 10, 8) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "ID",
                keyValue: 7);
        }
    }
}
