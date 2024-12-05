using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class userscanhavemanytodos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6121ae8a-8c39-4429-9f4d-adc260ad61a9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e92d0f26-5b76-4abd-9ed9-b6e360a6b4d8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d4e23ea-1fed-487b-afe1-9e784b52bb4f", null, "Admin", "ADMIN" },
                    { "daacef48-c8e9-49cb-8e46-d74bdf24f9b4", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d4e23ea-1fed-487b-afe1-9e784b52bb4f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "daacef48-c8e9-49cb-8e46-d74bdf24f9b4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6121ae8a-8c39-4429-9f4d-adc260ad61a9", null, "Admin", "ADMIN" },
                    { "e92d0f26-5b76-4abd-9ed9-b6e360a6b4d8", null, "User", "USER" }
                });
        }
    }
}
