using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tambolo.Migrations
{
    /// <inheritdoc />
    public partial class AddCartTableColsToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Amount",
                table: "Carts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "CartHeaderId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CartHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartHeaders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "51ec60a1-c7f1-48ea-a284-6f543d1d0a21", "4", "Vendor", "VENDOR" },
                    { "8b6aed40-4646-4990-9bd9-e2d11ac8a856", "2", "Admin", "ADMIN" },
                    { "e3c14cd4-b9a2-4cef-a213-25837579785c", "3", "User", "USER" },
                    { "ea50357a-33e8-4cfb-97ba-fde8b430a656", "1", "Super Admin", "SUPER ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CartHeaderId",
                table: "Carts",
                column: "CartHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_CartHeaders_UserId",
                table: "CartHeaders",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_CartHeaders_CartHeaderId",
                table: "Carts",
                column: "CartHeaderId",
                principalTable: "CartHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_CartHeaders_CartHeaderId",
                table: "Carts");

            migrationBuilder.DropTable(
                name: "CartHeaders");

            migrationBuilder.DropIndex(
                name: "IX_Carts_CartHeaderId",
                table: "Carts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51ec60a1-c7f1-48ea-a284-6f543d1d0a21");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b6aed40-4646-4990-9bd9-e2d11ac8a856");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3c14cd4-b9a2-4cef-a213-25837579785c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea50357a-33e8-4cfb-97ba-fde8b430a656");

            migrationBuilder.DropColumn(
                name: "CartHeaderId",
                table: "Carts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Carts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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
        }
    }
}
