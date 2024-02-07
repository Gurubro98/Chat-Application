using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class smallChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "412ec41f-fdfa-4e50-8132-cd738569ba5b", "AQAAAAEAACcQAAAAEIDSCJhyGNFVWDi+fC5uJ7QrLs/wDCRml+76yAQHYq8tP2wTvS2me2b8n4homWTi9g==", "af597790-ffbc-47bd-80e1-b260e91cd6da" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf15bcc6-f14f-4b19-985f-fee266af05fc", "AQAAAAEAACcQAAAAEHL5ZxbGeUJcJeUwKsvuz1q/7IvMh+PnNcYtQxfjfLUt4I4J/XiaHGctonYvpJLZ8A==", "ec05ee50-e982-4c28-8379-a4259dcb23cb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
