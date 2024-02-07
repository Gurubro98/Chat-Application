using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReceiverId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4942c4ce-02de-41bd-85ce-76ff6c726d8f", "AQAAAAEAACcQAAAAEEFMFzJlMfDCOIXCgHMnA/3LOp6ktnyCcGwaWhajhukViarR8ZSpHwsNZIbqarcU2g==", "9810c763-afe7-4d5c-ba21-b237fb592cf0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a28907b1-6079-4d58-931e-7c318da98905", "AQAAAAEAACcQAAAAEBZZZPr4mKfRY8KbwHL2K8VNyOOg9SgBKWBIY9vWbc7X2mZLDfBvTAQmZvetcU5/xg==", "096a1c25-575c-424b-8630-a0cd0bf4b56f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReceiverId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76340757-0c7e-42af-94ae-1439fbe0e6a9", "AQAAAAEAACcQAAAAEEmUAQWNzecLX9plVuxLGRAdj922JUQgSpAKskmsIRSUeFUeG4oe2duK9HVy1UT51w==", "f1a4e92d-5fa4-487d-8447-a51613a03f4b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "059fe5b4-0ef9-4cba-afe7-79be6000b165", "AQAAAAEAACcQAAAAEFxxIeUDayvH9uh0LlA0SCQddwNnGwVq4bnOtaboE+FclsKcuo7LVL//avCc7tQgZg==", "bb012edb-aa7d-4a42-96a4-449812021db1" });
        }
    }
}
