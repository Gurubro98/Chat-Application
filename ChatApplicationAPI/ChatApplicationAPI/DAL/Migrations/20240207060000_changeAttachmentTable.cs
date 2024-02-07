using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class changeAttachmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "createdTime",
                table: "Attachments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "916a4fac-a6bb-4ad5-b95b-880e89fed8ff", "AQAAAAEAACcQAAAAEK7rVforr+xB4B9PPEmr5OK18UOzf+Z57yBrVc3ZP+mdS5P/6ofEwMM2K7/OKxPxxw==", "17e2f117-13e7-4514-a57b-a4490be96a2c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5241d35-19b6-40a7-8f6a-2d00c7123fea", "AQAAAAEAACcQAAAAEPMQ2l1z+BCQQmRmaSwK6XHXb7ygxWUTBBEhlQHxl33IdE3NVcXErCE/BGh6m1tGJA==", "b4d3dabf-ac0b-427c-bf2d-213cbddef9a1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdTime",
                table: "Attachments");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d9da24d-ef2f-4621-ac7c-f6571cd5ad14", "AQAAAAEAACcQAAAAEFZPVcO/lyzwAypsrgvV/19eW4ozJbaO7Z8NgvUU8nRALx26OyfTG3HtRh7qz5rO6g==", "fe1a4e2b-589d-44ae-9fdb-b669bb159f10" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad03d0ba-4259-4fe5-a5c8-902d794e7fda", "AQAAAAEAACcQAAAAEMiovHpPtGKYxlx3jkKdC9wINOhOZMJHS4x2KFcQmUoioO+O2e+tP24fhIkJN8R3Iw==", "102bc848-7b22-47ce-96c1-9af54f3f36f4" });
        }
    }
}
