using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagmentSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Priority" },
                values: new object[] { new DateTime(2024, 3, 19, 12, 33, 28, 475, DateTimeKind.Local).AddTicks(962), "Low" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Priority", "Status" },
                values: new object[] { new DateTime(2024, 3, 19, 12, 33, 28, 475, DateTimeKind.Local).AddTicks(978), "Medium", "ToDo" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 19, 12, 33, 28, 475, DateTimeKind.Local).AddTicks(980));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "Status" },
                values: new object[] { new DateTime(2024, 3, 19, 12, 33, 28, 475, DateTimeKind.Local).AddTicks(982), "Done" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 19, 12, 33, 28, 475, DateTimeKind.Local).AddTicks(984));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Priority" },
                values: new object[] { new DateTime(2024, 3, 19, 11, 26, 57, 26, DateTimeKind.Local).AddTicks(9707), "High" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Priority", "Status" },
                values: new object[] { new DateTime(2024, 3, 19, 11, 26, 57, 26, DateTimeKind.Local).AddTicks(9721), "High", "InProgress" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 19, 11, 26, 57, 26, DateTimeKind.Local).AddTicks(9724));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "Status" },
                values: new object[] { new DateTime(2024, 3, 19, 11, 26, 57, 26, DateTimeKind.Local).AddTicks(9727), "InProgress" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 19, 11, 26, 57, 26, DateTimeKind.Local).AddTicks(9729));
        }
    }
}
