using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddIsReadColumnInGroupChatTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Messages",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "440f3077-0178-48db-8850-8696300de170", "AQAAAAEAACcQAAAAEOf36Ngkrmg0tq28Sr7kChCa9RrnpGaFxnZM6L5oh6Nv6ApCXKm13bs42sJEwqqyWw==", "22b2028c-8832-4dd1-a510-e510a879ba1e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e8b6178-d26b-4186-b43a-d30d0188e7fe", "AQAAAAEAACcQAAAAEMEM9DLmxqZZSZD6gVkQ0cVFve+L4RKMbR7/QA1QoCx7hVV+6leDbP2fESZrDpzVeg==", "3e4e4ff9-eb6e-4bf9-8d16-7c7298787677" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Messages");

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
    }
}
