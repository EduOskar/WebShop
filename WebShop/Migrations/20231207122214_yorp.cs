using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Api.Migrations;

/// <inheritdoc />
public partial class Yorp : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "UserName",
            table: "AspNetUsers",
            type: "nvarchar(256)",
            maxLength: 256,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(256)",
            oldMaxLength: 256);

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: 1,
            column: "ConcurrencyStamp",
            value: "e2c72f22-0ce7-4384-9064-36517af81465");

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: 2,
            column: "ConcurrencyStamp",
            value: "8a534feb-616e-4dc8-80e7-2ad53fdae442");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "UserName",
            table: "AspNetUsers",
            type: "nvarchar(256)",
            maxLength: 256,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(256)",
            oldMaxLength: 256,
            oldNullable: true);

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: 1,
            column: "ConcurrencyStamp",
            value: "5e4febf1-0f01-4d6d-bff9-f7567a935332");

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: 2,
            column: "ConcurrencyStamp",
            value: "2f851d8a-7d22-4fc6-953b-d7c20a663433");
    }
}
