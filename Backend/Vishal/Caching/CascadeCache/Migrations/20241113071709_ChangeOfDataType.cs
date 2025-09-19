using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CascadeCache.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOfDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Employees",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1001,
                column: "DateOfBirth",
                value: new DateOnly(1998, 5, 10));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1002,
                column: "DateOfBirth",
                value: new DateOnly(1995, 5, 10));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1003,
                column: "DateOfBirth",
                value: new DateOnly(2000, 5, 10));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1001,
                column: "DateOfBirth",
                value: new DateTime(1998, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1002,
                column: "DateOfBirth",
                value: new DateTime(1995, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1003,
                column: "DateOfBirth",
                value: new DateTime(2000, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
