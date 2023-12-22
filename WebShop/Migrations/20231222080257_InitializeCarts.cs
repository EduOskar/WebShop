using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Api.Migrations;

/// <inheritdoc />
public partial class InitializeCarts : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Qty",
            table: "OrderItems",
            newName: "Quantity");

        migrationBuilder.InsertData(
            table: "Carts",
            columns: new[] { "Id", "UserId" },
            values: new object[] { 1, 1 });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Carts",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.RenameColumn(
            name: "Quantity",
            table: "OrderItems",
            newName: "Qty");
    }
}
