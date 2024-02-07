using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class changeMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9cc94d81-d940-44dd-9712-899559261bca", "AQAAAAEAACcQAAAAEJUohM1053XImf0gsoJdRUELt7hsaf1jIt/gZ0SfAngi2OpcDwwFtqPFR7rXwXKsuw==", "539a48b7-8763-4078-99c0-2717567b8b54" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fa607998-0bf4-4e7d-bb70-08410cd4b44c", "AQAAAAEAACcQAAAAEFcL/MCO1iK+Q4Tt1PwBFRoB/pAW0+ApUJoYFBtyYYKLMlwYCXoPTS4bvj1+ushIiA==", "ddaa84c1-6330-4df9-86c0-6ea480854772" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Messages",
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
                values: new object[] { "916a4fac-a6bb-4ad5-b95b-880e89fed8ff", "AQAAAAEAACcQAAAAEK7rVforr+xB4B9PPEmr5OK18UOzf+Z57yBrVc3ZP+mdS5P/6ofEwMM2K7/OKxPxxw==", "17e2f117-13e7-4514-a57b-a4490be96a2c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5241d35-19b6-40a7-8f6a-2d00c7123fea", "AQAAAAEAACcQAAAAEPMQ2l1z+BCQQmRmaSwK6XHXb7ygxWUTBBEhlQHxl33IdE3NVcXErCE/BGh6m1tGJA==", "b4d3dabf-ac0b-427c-bf2d-213cbddef9a1" });
        }
    }
}
