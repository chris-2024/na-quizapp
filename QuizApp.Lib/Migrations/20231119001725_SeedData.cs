using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizApp.Lib.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName" },
                values: new object[,]
                {
                    { 1, "C#" },
                    { 2, "Fruits" },
                    { 3, "Potatoes" },
                    { 4, "Technology" },
                    { 5, "Other" }
                });

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "DifficultyID", "DifficultyName" },
                values: new object[,]
                {
                    { 1, "Normal" },
                    { 2, "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageID", "LanguageName" },
                values: new object[,]
                {
                    { 1, "English" },
                    { 2, "Swedish" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleID", "RoleName" },
                values: new object[,]
                {
                    { 1, "Registered" },
                    { 2, "Guest" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "DifficultyID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "DifficultyID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "UserRoleID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "UserRoleID",
                keyValue: 2);
        }
    }
}
