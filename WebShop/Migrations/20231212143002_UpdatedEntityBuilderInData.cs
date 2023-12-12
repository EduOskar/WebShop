using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebShop.Api.Migrations;

/// <inheritdoc />
public partial class UpdatedEntityBuilderInData : Migration
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
            values: new object[] { "ff93c01e-ff73-48f7-bb47-5c69116084d8", "AQAAAAIAAYagAAAAEEJohn/3VV3pSRCGcWSy+R1xBaIIwYyx4ox+X07VXLnHNxd7xHwo0iQBOWdHziMp3g==" });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "ConcurrencyStamp", "PasswordHash" },
            values: new object[] { "2d40a8a8-a36b-4994-be17-f7829791ab68", "AQAAAAIAAYagAAAAEKuOMJLjiHWdChQaG3Spr+AZiRV1MkvQBBwNjdPbbNYXoDEVL6RqIqVyQT0NeR0b4A==" });
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
            values: new object[] { "d3273e7c-4541-4e20-a593-8194c8743b65", "AQAAAAIAAYagAAAAEN3FTuZECnAurA0FQYZHu6ruuD9Qe3Z79jBauaKOE5yDr0Vw3Te7kuPhEWyf95+PDw==" });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "ConcurrencyStamp", "PasswordHash" },
            values: new object[] { "29ecf68a-e806-4473-b34a-ff6e7a29e986", "AQAAAAIAAYagAAAAEGwPMGh1FHxJ0+bhgdQXj7pq18NUkmlw7FwzHk4oWNSSczDN3ukjLYrkfSySAG4hvg==" });
    }
}
