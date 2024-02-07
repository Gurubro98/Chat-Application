using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddIsDeletedCoulmnInRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Requests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MutualRelations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e433db18-fdd9-413f-b320-df86b2f63c8a", "AQAAAAEAACcQAAAAEF4SeipkbIVHrzmXrFjeg0OQSfrM37wwaywF9qQsMM9TxR7Uoh8f3BJHw9CrtTRMew==", "e4cc584f-ad14-49b5-81db-b0936b901ad5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ab680f7-055d-4a7d-a02d-2bb773b3112a", "AQAAAAEAACcQAAAAEJejY0P495mVFeX++QpkNtksXsuoPcYaluQ0khFaXi5o5nuSULpOm0WXf5U7m8fYfQ==", "140082c9-9a0d-4251-b1cd-df27f3d14df4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MutualRelations");

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
    }
}
