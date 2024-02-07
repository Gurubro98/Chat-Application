using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class MessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_ReceiverId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_SenderId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_GroupChat_GroupId",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.RenameIndex(
                name: "IX_Message_SenderId",
                table: "Messages",
                newName: "IX_Messages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ReceiverId",
                table: "Messages",
                newName: "IX_Messages_ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_GroupId",
                table: "Messages",
                newName: "IX_Messages_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "MessageId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_GroupChat_GroupId",
                table: "Messages",
                column: "GroupId",
                principalTable: "GroupChat",
                principalColumn: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_GroupChat_GroupId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SenderId",
                table: "Message",
                newName: "IX_Message_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ReceiverId",
                table: "Message",
                newName: "IX_Message_ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_GroupId",
                table: "Message",
                newName: "IX_Message_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "MessageId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dbf5161a-7e06-4e31-99bc-da57e4847a81", "AQAAAAEAACcQAAAAEN0zoqyiE1UA8kfIN6trnTRS1kJtW/x+6VZdmP+eBUDSxQ2dpT08Xms6XoAADiN9gw==", "310d3d5a-2418-440b-bab2-0b63eb71e4e4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b983ee3-30a3-4acd-b1cd-d2e58673a54f", "AQAAAAEAACcQAAAAEN9Ca2lqZgjTwCuM7NvtkRbDFML+5DrBUfqAPOjkA0x6XyJGkdYztTiatGLAQ9TN3Q==", "e94a6c7a-4975-4c83-8b2c-f9af06a898fd" });

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_ReceiverId",
                table: "Message",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_SenderId",
                table: "Message",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_GroupChat_GroupId",
                table: "Message",
                column: "GroupId",
                principalTable: "GroupChat",
                principalColumn: "GroupId");
        }
    }
}
