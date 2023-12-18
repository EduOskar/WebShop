using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebShop.Api.Migrations;

/// <inheritdoc />
public partial class Roles : Migration
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
            values: new object[] { "b291f094-febb-44a0-b5c6-ca50ef4b94d6", "AQAAAAIAAYagAAAAEKlYTaj/AAmeklBcd1YLelAEwvQiA1ybU8fbUCpvJGQjMgjUlriTsnUqwaQlMZqsDw==" });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "ConcurrencyStamp", "PasswordHash" },
            values: new object[] { "82a190d3-703c-4d00-bdb7-81bbcf5abb18", "AQAAAAIAAYagAAAAEP7RJ3DrhiLq3vSILQjA+/ov2ucSAoFTTPcW9iQ3CR3nrOFpTPTQEuXVDAUwNXg5+w==" });
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
            values: new object[] { "da59b8f2-05b2-40db-8c3c-37d5431e3de2", "AQAAAAIAAYagAAAAEE8UreoCo/HexcC1OZ2WyFR6ZD0fgoJ6fDdAPmVvKpngacOqcFptQ5hE78E9e6fLlg==" });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "ConcurrencyStamp", "PasswordHash" },
            values: new object[] { "b9ccc3ba-38fe-4b0a-81cd-82aabf4d39f5", "AQAAAAIAAYagAAAAELtb4g33FlA9tIM1ECrGHxOc7x6XtTxKw0ISofDMxv+1fs4aPBaXEtX7+Ajjyab9BA==" });
    }
}
