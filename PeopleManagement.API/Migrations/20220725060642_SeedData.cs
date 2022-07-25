using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleManagement.API.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[] { 12345678, "Ana", "Garcia" });

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "Id", "Entry", "Exit", "ExpectedDuration", "IsActive", "Motive", "PersonId" },
                values: new object[] { 1, new DateTime(2022, 7, 25, 7, 6, 42, 102, DateTimeKind.Local).AddTicks(299), null, 2, true, "teste de entrada", 12345678 });

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "Id", "Entry", "Exit", "ExpectedDuration", "IsActive", "Motive", "PersonId" },
                values: new object[] { 2, new DateTime(2022, 7, 25, 7, 6, 42, 102, DateTimeKind.Local).AddTicks(337), new DateTime(2022, 7, 25, 7, 6, 42, 102, DateTimeKind.Local).AddTicks(339), 1, false, "teste de entrada 2", 12345678 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 12345678);
        }
    }
}
