using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tambolo.Migrations
{
    /// <inheritdoc />
    public partial class UseSingleCartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValue: "50ca68e5-7a1c-4e82-80b5-e479b2baab11");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73c21c0b-8724-4e28-a2d8-e687ecdb1061");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96dbcbf6-d35a-4982-a8bd-d7e780479d3a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a78c0f66-c7a2-4f30-bc38-ec8fa695f383");

            migrationBuilder.DropColumn(
                name: "CartHeaderId",
                table: "Carts");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "23920ca5-cfe2-43c4-80d3-cbbde648cc76", "4", "Vendor", "VENDOR" },
                    { "5651be10-a78d-4a58-bf6e-9a9cfc7d9664", "2", "Admin", "ADMIN" },
                    { "699e334c-5575-4937-8222-8057321edb95", "3", "User", "USER" },
                    { "6e48034d-64b3-4bd2-bfa1-46cc4f41475d", "1", "Super Admin", "SUPER ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23920ca5-cfe2-43c4-80d3-cbbde648cc76");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5651be10-a78d-4a58-bf6e-9a9cfc7d9664");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "699e334c-5575-4937-8222-8057321edb95");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e48034d-64b3-4bd2-bfa1-46cc4f41475d");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Carts");

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
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "50ca68e5-7a1c-4e82-80b5-e479b2baab11", "2", "Admin", "ADMIN" },
                    { "73c21c0b-8724-4e28-a2d8-e687ecdb1061", "3", "User", "USER" },
                    { "96dbcbf6-d35a-4982-a8bd-d7e780479d3a", "1", "Super Admin", "SUPER ADMIN" },
                    { "a78c0f66-c7a2-4f30-bc38-ec8fa695f383", "4", "Vendor", "VENDOR" }
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
                principalColumn: "Id");
        }
    }
}
