using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class ChangesInRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsTakeAction",
                table: "Requests",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aea9bcab-66cf-4743-8d64-ecd72ed48b46", "AQAAAAEAACcQAAAAEBoiXxO5nhGzFBhe6x8v3UHgbntVP4mgHD4NUGQxPAE+zKjzhZZDoRsz8GDwdRGZYA==", "7ea2394b-5299-4de8-baeb-be01542e6250" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0bca28de-2be8-4a85-8163-8153133a9ccb", "AQAAAAEAACcQAAAAEIA9xNAJSXG3NWhX4IKxZ/gybIsyayCzNB5+icS+Ngsx+nF4+xj36JS6Zi/beWE6rQ==", "7198ec21-7f84-4d58-9ea7-4e3849a64a26" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Requests");

            migrationBuilder.AlterColumn<int>(
                name: "IsTakeAction",
                table: "Requests",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49033daf-7e3d-4acc-afa3-aefb7c0edf2e", "AQAAAAEAACcQAAAAEDzupHfI03TGW7zc9NHB5M5e3HQCkqcRKTpGqsRno8CQPmW2xPC82zOz37FzFEeCSQ==", "2c08dcd3-e9fa-41ee-ab5b-d1b80ee3fe3f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8383f22-5e68-4823-95ba-0f013d137543", "AQAAAAEAACcQAAAAEBcwtbG1O4yHxNM1jI6VROFVONgv6K/Ntt26nwMVal3w9jKV/vv6O2EcVQHEd5VW+Q==", "1cbfb819-dabd-4053-9d09-0a3bbba2dbed" });
        }
    }
}
