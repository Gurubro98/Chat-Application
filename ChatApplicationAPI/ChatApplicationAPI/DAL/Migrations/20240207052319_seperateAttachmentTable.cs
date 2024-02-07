using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class seperateAttachmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Messages");

            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.AttachmentId);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AttachmentId",
                table: "Messages",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Attachments_AttachmentId",
                table: "Messages",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "AttachmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Attachments_AttachmentId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AttachmentId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

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
    }
}
