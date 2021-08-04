using Microsoft.EntityFrameworkCore.Migrations;

namespace IotRestFullApi.Migrations
{
    public partial class fixactionandcommand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Action");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Command",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Command",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Action",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
