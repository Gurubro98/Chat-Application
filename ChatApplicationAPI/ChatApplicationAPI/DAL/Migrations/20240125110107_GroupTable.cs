using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class GroupTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChat_AspNetUsers_SenderId",
                table: "GroupChat");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupChat_AspNetUsers_UserId",
                table: "GroupChat");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_AspNetUsers_UserId",
                table: "UserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_GroupChat_GroupId",
                table: "UserGroup");

            migrationBuilder.DropIndex(
                name: "IX_GroupChat_SenderId",
                table: "GroupChat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroup",
                table: "UserGroup");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "GroupChat");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "GroupChat");

            migrationBuilder.RenameTable(
                name: "UserGroup",
                newName: "UserGroups");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroup_GroupId",
                table: "UserGroups",
                newName: "IX_UserGroups_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroups",
                table: "UserGroups",
                columns: new[] { "UserId", "GroupId" });

            migrationBuilder.CreateTable(
                name: "GroupUserChat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                values: new object[] { "76340757-0c7e-42af-94ae-1439fbe0e6a9", "AQAAAAEAACcQAAAAEEmUAQWNzecLX9plVuxLGRAdj922JUQgSpAKskmsIRSUeFUeG4oe2duK9HVy1UT51w==", "f1a4e92d-5fa4-487d-8447-a51613a03f4b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "059fe5b4-0ef9-4cba-afe7-79be6000b165", "AQAAAAEAACcQAAAAEFxxIeUDayvH9uh0LlA0SCQddwNnGwVq4bnOtaboE+FclsKcuo7LVL//avCc7tQgZg==", "bb012edb-aa7d-4a42-96a4-449812021db1" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserChat_GroupId",
                table: "GroupUserChat",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserChat_SenderId",
                table: "GroupUserChat",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChat_AspNetUsers_UserId",
                table: "GroupChat",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_AspNetUsers_UserId",
                table: "UserGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_GroupChat_GroupId",
                table: "UserGroups",
                column: "GroupId",
                principalTable: "GroupChat",
                principalColumn: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChat_AspNetUsers_UserId",
                table: "GroupChat");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_AspNetUsers_UserId",
                table: "UserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_GroupChat_GroupId",
                table: "UserGroups");

            migrationBuilder.DropTable(
                name: "GroupUserChat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroups",
                table: "UserGroups");

            migrationBuilder.RenameTable(
                name: "UserGroups",
                newName: "UserGroup");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroups_GroupId",
                table: "UserGroup",
                newName: "IX_UserGroup_GroupId");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "GroupChat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "GroupChat",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroup",
                table: "UserGroup",
                columns: new[] { "UserId", "GroupId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a342994-51ca-453a-916d-aab54096f97b", "AQAAAAEAACcQAAAAEDn2zlQPIkA6ZIa36/KBMGBypQYoHIB7Z24axtz/KotBTxcdkoGZaSitNgnqJM81mA==", "6bead10d-f236-4a0f-a29f-932aee6cee8f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cbf38d1d-5220-41ae-a6ff-cc4f3b6198c0", "AQAAAAEAACcQAAAAEMPsBj6Oj3bIFji6orVF+nNgvxYVp0SnfdRhybLL77B343FNzkL+ba0Un1NOG3MNqw==", "09899f7b-ad4c-4946-99f2-a7a40190f5cb" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupChat_SenderId",
                table: "GroupChat",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChat_AspNetUsers_SenderId",
                table: "GroupChat",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChat_AspNetUsers_UserId",
                table: "GroupChat",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_AspNetUsers_UserId",
                table: "UserGroup",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_GroupChat_GroupId",
                table: "UserGroup",
                column: "GroupId",
                principalTable: "GroupChat",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
