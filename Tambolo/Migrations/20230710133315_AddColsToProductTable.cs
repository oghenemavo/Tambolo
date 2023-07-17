using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tambolo.Migrations
{
    /// <inheritdoc />
    public partial class AddColsToProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a430b54-6ad8-406a-9a33-0185e6ff0923");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2eb361f0-baf6-4606-96bb-ccb8cbb0ffdf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84908a77-fbdf-4b84-bd1e-e05935438f3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2d8ede7-e29c-4b46-8d1a-c8461a18f4dc");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0c1abc67-0b52-4d24-8e42-57d690073726", "2", "Admin", "ADMIN" },
                    { "6c6501ef-a5cf-42ac-8939-5472c7dc6c01", "4", "Vendor", "VENDOR" },
                    { "72600eff-9b73-4b6f-873a-6449712410eb", "1", "Super Admin", "SUPER ADMIN" },
                    { "8fae2a6b-b9f1-4819-9d8b-a170143704fc", "3", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c1abc67-0b52-4d24-8e42-57d690073726");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c6501ef-a5cf-42ac-8939-5472c7dc6c01");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72600eff-9b73-4b6f-873a-6449712410eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fae2a6b-b9f1-4819-9d8b-a170143704fc");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a430b54-6ad8-406a-9a33-0185e6ff0923", "1", "Super Admin", "SUPER ADMIN" },
                    { "2eb361f0-baf6-4606-96bb-ccb8cbb0ffdf", "3", "User", "USER" },
                    { "84908a77-fbdf-4b84-bd1e-e05935438f3c", "4", "Vendor", "VENDOR" },
                    { "b2d8ede7-e29c-4b46-8d1a-c8461a18f4dc", "2", "Admin", "ADMIN" }
                });
        }
    }
}
