using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tambolo.Migrations
{
    /// <inheritdoc />
    public partial class AddSeederMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13932ea0-e3d5-4a71-aa5c-d22c0bf80d31", "2", "Admin", "ADMIN" },
                    { "28a55b68-465d-47b7-8128-dcac060973dd", "3", "User", "USER" },
                    { "95957799-c3f5-4e0d-a3af-92131ea8d635", "1", "Super Admin", "SUPER ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13932ea0-e3d5-4a71-aa5c-d22c0bf80d31");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28a55b68-465d-47b7-8128-dcac060973dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95957799-c3f5-4e0d-a3af-92131ea8d635");
        }
    }
}
