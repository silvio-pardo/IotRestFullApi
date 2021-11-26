using Microsoft.EntityFrameworkCore.Migrations;

namespace IotRestFullApi.Migrations
{
    public partial class UpdateDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Action_Device_DeviceID",
                table: "Action");

            migrationBuilder.DropForeignKey(
                name: "FK_Command_Device_DeviceID",
                table: "Command");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Device_DeviceID",
                table: "Stats");

            migrationBuilder.RenameColumn(
                name: "DeviceID",
                table: "Stats",
                newName: "DeviceUid");

            migrationBuilder.RenameIndex(
                name: "IX_Stats_DeviceID",
                table: "Stats",
                newName: "IX_Stats_DeviceUid");

            migrationBuilder.RenameColumn(
                name: "DeviceID",
                table: "Command",
                newName: "DeviceUid");

            migrationBuilder.RenameIndex(
                name: "IX_Command_DeviceID",
                table: "Command",
                newName: "IX_Command_DeviceUid");

            migrationBuilder.RenameColumn(
                name: "DeviceID",
                table: "Action",
                newName: "DeviceUid");

            migrationBuilder.RenameIndex(
                name: "IX_Action_DeviceID",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "DeviceID");

            migrationBuilder.RenameIndex(
                name: "IX_Stats_DeviceUid",
                table: "Stats",
                newName: "IX_Stats_DeviceID");

            migrationBuilder.RenameColumn(
                name: "DeviceUid",
                table: "Command",
                newName: "DeviceID");

            migrationBuilder.RenameIndex(
                name: "IX_Command_DeviceUid",
                table: "Command",
                newName: "IX_Command_DeviceID");

            migrationBuilder.RenameColumn(
                name: "DeviceUid",
                table: "Action",
                newName: "DeviceID");

            migrationBuilder.RenameIndex(
                name: "IX_Action_DeviceUid",
                table: "Action",
                newName: "IX_Action_DeviceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Action_Device_DeviceID",
                table: "Action",
                column: "DeviceID",
                principalTable: "Device",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Command_Device_DeviceID",
                table: "Command",
                column: "DeviceID",
                principalTable: "Device",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Device_DeviceID",
                table: "Stats",
                column: "DeviceID",
                principalTable: "Device",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
