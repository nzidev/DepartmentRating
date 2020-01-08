using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DepartmentRatingDAL.Migrations
{
    public partial class forth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2019, 12, 11, 10, 57, 42, 578, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2019, 12, 11, 9, 40, 24, 0, DateTimeKind.Local));
        }
    }
}
