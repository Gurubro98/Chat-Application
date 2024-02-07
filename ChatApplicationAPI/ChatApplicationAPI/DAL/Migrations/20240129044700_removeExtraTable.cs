using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class removeExtraTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupUserChat");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3664c66d-f290-45a4-801b-bbb5ee2098c8", "AQAAAAEAACcQAAAAEDDRqK8vHnWxfW6cHA+rsFj0KzNGCJ2zHc0pGD6fGcrP37ah8cVZFywigj9+xbmBCA==", "57d62182-7580-491b-9cff-db5e11fd316f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5dccfbdc-b246-4bb7-a262-6325f241fcc8", "AQAAAAEAACcQAAAAEES0m4Z/J6WtlxSyk8D/Ap6c0YhbITPQz+kf8pCzxpvyFr29LFUshtUZo+xsYKvfOw==", "065da2eb-eb96-4a7c-9385-0e6396661ac5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupUserChat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUserChat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupUserChat_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupUserChat_GroupChat_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupChat",
                        principalColumn: "GroupId");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserChat_GroupId",
                table: "GroupUserChat",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserChat_SenderId",
                table: "GroupUserChat",
                column: "SenderId");
        }
    }
}
