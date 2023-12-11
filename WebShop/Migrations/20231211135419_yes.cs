using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Api.Migrations;

/// <inheritdoc />
public partial class Yes : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: 1,
            column: "ConcurrencyStamp",
            value: "68823cae-1150-421c-9e8d-e38929952364");

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: 2,
            column: "ConcurrencyStamp",
            value: "08d9a0c0-0b7a-4846-b353-a5e172e7f965");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: 1,
            column: "ConcurrencyStamp",
            value: "98411210-ee33-42fb-a51f-1e35fcfd2887");

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: 2,
            column: "ConcurrencyStamp",
            value: "0dd203f5-6e83-4559-bfbd-88ef2749e94c");
    }
}
