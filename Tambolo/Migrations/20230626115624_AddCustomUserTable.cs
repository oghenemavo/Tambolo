using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tambolo.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "081e77b8-ad8e-4733-8d76-37e4a0512598", "1", "Super Admin", "SUPER ADMIN" },
                    { "0cdfbbcc-aa65-45a7-abd7-86f93fb6a52c", "2", "Admin", "ADMIN" },
                    { "3d02977a-b546-43ab-a02a-048b49d72a09", "3", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "081e77b8-ad8e-4733-8d76-37e4a0512598");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0cdfbbcc-aa65-45a7-abd7-86f93fb6a52c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d02977a-b546-43ab-a02a-048b49d72a09");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

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
    }
}
