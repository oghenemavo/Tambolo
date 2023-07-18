using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tambolo.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartHeaders_AspNetUsers_UserId",
                table: "CartHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_CartHeaders_CartHeaderId",
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

            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "CartHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6d3e7653-c80c-4a4f-a73b-e18dcf6d968c", "1", "Super Admin", "SUPER ADMIN" },
                    { "74de244e-1971-48fa-801e-debdba25e282", "2", "Admin", "ADMIN" },
                    { "c5d55f41-da36-4399-aba1-3547878e9726", "3", "User", "USER" },
                    { "cff98fc0-c0e3-4933-8dfa-ad51009f4255", "4", "Vendor", "VENDOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CartHeaders_AspNetUsers_UserId",
                table: "CartHeaders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_CartHeaders_CartHeaderId",
                table: "Carts",
                column: "CartHeaderId",
                principalTable: "CartHeaders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartHeaders_AspNetUsers_UserId",
                table: "CartHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_CartHeaders_CartHeaderId",
                table: "Carts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d3e7653-c80c-4a4f-a73b-e18dcf6d968c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74de244e-1971-48fa-801e-debdba25e282");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5d55f41-da36-4399-aba1-3547878e9726");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cff98fc0-c0e3-4933-8dfa-ad51009f4255");

            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "CartHeaders");

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

            migrationBuilder.AddForeignKey(
                name: "FK_CartHeaders_AspNetUsers_UserId",
                table: "CartHeaders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_CartHeaders_CartHeaderId",
                table: "Carts",
                column: "CartHeaderId",
                principalTable: "CartHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
