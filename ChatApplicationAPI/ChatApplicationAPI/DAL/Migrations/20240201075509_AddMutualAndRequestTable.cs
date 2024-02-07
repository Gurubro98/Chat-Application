using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddMutualAndRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MutualRelations",
                columns: table => new
                {
                    MutualId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MutualRelations", x => new { x.MutualId, x.UserId });
                    table.ForeignKey(
                        name: "FK_MutualRelations_AspNetUsers_MutualId",
                        column: x => x.MutualId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MutualRelations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsTakeAction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_MutualRelations_UserId",
                table: "MutualRelations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ReceiverId",
                table: "Requests",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_SenderId",
                table: "Requests",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MutualRelations");

            migrationBuilder.DropTable(
                name: "Requests");

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
    }
}
