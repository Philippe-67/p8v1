using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservice.Authentication.Migrations
{
    public partial class migVerif : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48b47b3a-0f26-4d07-98ee-1ad4cf7f723e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c2c033d-81b3-455b-9338-0fe3c61bd134");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a65dfdda-b39a-4b15-aedd-dafa043dbcaa");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1435edcc-d099-4bfa-8fee-c4485d461cd2", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2679c9ea-af2d-44ac-b7e5-bb9210ea6b0a", "2", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7463559e-83ac-45eb-8b8b-d3cc8895af99", "3", "RH", "RH" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1435edcc-d099-4bfa-8fee-c4485d461cd2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2679c9ea-af2d-44ac-b7e5-bb9210ea6b0a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7463559e-83ac-45eb-8b8b-d3cc8895af99");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "48b47b3a-0f26-4d07-98ee-1ad4cf7f723e", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c2c033d-81b3-455b-9338-0fe3c61bd134", "3", "RH", "RH" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a65dfdda-b39a-4b15-aedd-dafa043dbcaa", "2", "User", "User" });
        }
    }
}
