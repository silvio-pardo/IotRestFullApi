using Microsoft.EntityFrameworkCore.Migrations;

namespace IotRestFullApi.Migrations
{
    public partial class addkeysproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Action_Device_DeviceUid",
                table: "Action");

            migrationBuilder.DropForeignKey(
                name: "FK_Command_Device_DeviceUid",
                table: "Command");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Device_DeviceUid",
                table: "Stats");

            migrationBuilder.RenameColumn(
                name: "DeviceUid",
                table: "Stats",
                newName: "DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Stats_DeviceUid",
                table: "Stats",
                newName: "IX_Stats_DeviceId");

            migrationBuilder.RenameColumn(
                name: "DeviceUid",
                table: "Command",
                newName: "DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Command_DeviceUid",
                table: "Command",
                newName: "IX_Command_DeviceId");

            migrationBuilder.RenameColumn(
                name: "DeviceUid",
                table: "Action",
                newName: "DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Action_DeviceUid",
                table: "Action",
                newName: "IX_Action_DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Action_Device_DeviceId",
                table: "Action",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Command_Device_DeviceId",
                table: "Command",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Device_DeviceId",
                table: "Stats",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Action_Device_DeviceId",
                table: "Action");

            migrationBuilder.DropForeignKey(
                name: "FK_Command_Device_DeviceId",
                table: "Command");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Device_DeviceId",
                table: "Stats");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "Stats",
                newName: "DeviceUid");

            migrationBuilder.RenameIndex(
                name: "IX_Stats_DeviceId",
                table: "Stats",
                newName: "IX_Stats_DeviceUid");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "Command",
                newName: "DeviceUid");

            migrationBuilder.RenameIndex(
                name: "IX_Command_DeviceId",
                table: "Command",
                newName: "IX_Command_DeviceUid");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "Action",
                newName: "DeviceUid");

            migrationBuilder.RenameIndex(
                name: "IX_Action_DeviceId",
                table: "Action",
                newName: "IX_Action_DeviceUid");

            migrationBuilder.AddForeignKey(
                name: "FK_Action_Device_DeviceUid",
                table: "Action",
                column: "DeviceUid",
                principalTable: "Device",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Command_Device_DeviceUid",
                table: "Command",
                column: "DeviceUid",
                principalTable: "Device",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Device_DeviceUid",
                table: "Stats",
                column: "DeviceUid",
                principalTable: "Device",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
