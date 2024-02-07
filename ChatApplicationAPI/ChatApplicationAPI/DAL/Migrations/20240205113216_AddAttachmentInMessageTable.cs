using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddAttachmentInMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18883eae-6a9f-464d-a065-3fb73d443763", "AQAAAAEAACcQAAAAEB0Wu6FG4H+poshiemqAVRN6QkZWy92rX0rSSgVmGPzer4FU3lnZwUnwVzKM6vN5Pw==", "751bf347-6540-4fb6-a384-be322641d61d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96e455bd-218e-4017-b715-15df5b719569", "AQAAAAEAACcQAAAAEDUYlMOWYq2F0rbh44x6jkYeMmKbcvq1jYGzy4lq7jGtn2a+6XWPG1i9UrpkWuim4Q==", "d0b7fce7-54a1-4d06-8707-c28b74ef173a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

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
    }
}
