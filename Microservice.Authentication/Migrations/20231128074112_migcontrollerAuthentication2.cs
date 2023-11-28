using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservice.Authentication.Migrations
{
    public partial class migcontrollerAuthentication2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
