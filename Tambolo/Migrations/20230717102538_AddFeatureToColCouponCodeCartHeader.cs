using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tambolo.Migrations
{
    /// <inheritdoc />
    public partial class AddFeatureToColCouponCodeCartHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "CouponCode",
                table: "CartHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "CouponCode",
                table: "CartHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
        }
    }
}
