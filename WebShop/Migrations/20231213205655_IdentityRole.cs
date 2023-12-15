using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebShop.Api.Migrations
{
    /// <inheritdoc />
    public partial class IdentityRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 2, 2 }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1d23b768-c353-4575-959b-aead3da4351d", "AQAAAAIAAYagAAAAEAdlIvvG21GxJgUx2CatgctcHgatCb3iE1zmeJMMytzLd0PzR6tRFjrXBRlU8v9VKw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "63cece21-f089-4a14-911f-6a4ad8b13aaa", "AQAAAAIAAYagAAAAEM6YcBgqi2QFiZg9q76ptR+qC0aVdAYoNm7GMmj5sOXVPiEgOluiemWzJyC4PliCLg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "26f8fe26-db5b-4470-a91c-968d43e27c0e", "AQAAAAIAAYagAAAAEFzoP7Ga1/hThhEReTSf2btTFGpBjMCC5V8EVc3yj0GczqCgwcfgRywdsu/C+yb6wA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "71bb2056-bd28-40a5-91ef-db8438e0807e", "AQAAAAIAAYagAAAAEJdbSREATYnywSYtm4h+8CmzKAZyPNn6ZxYt7WjOmK4A02N+msQeCzh0JSZWJymoyg==" });
        }
    }
}
