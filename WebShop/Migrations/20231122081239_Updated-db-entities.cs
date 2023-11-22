﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Api.Migrations;

/// <inheritdoc />
public partial class Updateddbentities : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "Credit",
            table: "Users",
            type: "decimal(18,2)",
            nullable: true,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,2)");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Products",
            type: "nvarchar(500)",
            maxLength: 500,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(500)",
            oldMaxLength: 500,
            oldNullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "Credit",
            table: "Users",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,2)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Products",
            type: "nvarchar(500)",
            maxLength: 500,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(500)",
            oldMaxLength: 500);
    }
}
